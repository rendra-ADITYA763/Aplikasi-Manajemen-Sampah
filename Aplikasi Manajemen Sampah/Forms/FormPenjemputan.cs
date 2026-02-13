using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Aplikasi_Manajemen_Sampah.Models;
using Microsoft.Web.WebView2.Core;

namespace Aplikasi_Manajemen_Sampah.Forms
{
    public partial class FormPenjemputan : Form
    {
        private User currentUser;

        private List<Sampah> listSampah = new();
        private List<User> listPetugas = new();
        private List<Penjemputan> listPenjemputan = new();

        private string selectedId = "";

        // 🔥 Tambahan untuk fullscreen
        private bool isFullscreen = false;
        private Rectangle originalBounds;

        public FormPenjemputan(User user)
        {
            currentUser = user;

            InitializeComponent();
            SetupEvents();
            LoadDummyData();
        }

        private void SetupEvents()
        {
            btnSimpan.Click += BtnSimpan_Click;
            btnHapus.Click += BtnHapus_Click;
            btnClear.Click += (s, e) => ClearInputs();
            dgvPenjemputan.CellClick += DgvPenjemputan_CellClick;
            cboSampah.SelectedIndexChanged += CboSampah_SelectedIndexChanged;

            // 🔥 Event Fullscreen Button (Pastikan sudah buat button bernama btnFullscreen)
            btnFullscreen.Click += BtnFullscreen_Click;

            this.Load += FormPenjemputan_Load;

            cboStatus.SelectedIndex = 0;
        }

        // 🔥 Supaya Designer tidak error
        private void panelInput_Paint(object sender, PaintEventArgs e)
        {
        }

        // ================== LOAD MAP ==================
        private async void FormPenjemputan_Load(object sender, EventArgs e)
        {
            try
            {
                await webViewMap.EnsureCoreWebView2Async(null);

                string html = @"
        <!DOCTYPE html>
        <html>
        <head>
            <meta http-equiv='X-UA-Compatible' content='IE=edge'/>
            <style>
                html, body { margin:0; padding:0; height:100%; }
                #map { height:100%; width:100%; }
            </style>
            <script src='https://maps.googleapis.com/maps/api/js'></script>
            <script>
                function initMap() {
                    var bandung = { lat: -6.9175, lng: 107.6191 };

                    var map = new google.maps.Map(document.getElementById('map'), {
                        zoom: 12,
                        center: bandung
                    });

                    var locations = [
                        { lat: -6.9025, lng: 107.6186, title: 'Gedung Sate - Titik Penjemputan' },
                        { lat: -6.9218, lng: 107.6079, title: 'Alun-Alun Bandung - Titik Penjemputan' },
                        { lat: -6.8885, lng: 107.6130, title: 'Dago - Titik Penjemputan' }
                    ];

                    locations.forEach(function(loc) {
                        new google.maps.Marker({
                            position: { lat: loc.lat, lng: loc.lng },
                            map: map,
                            title: loc.title
                        });
                    });
                }
            </script>
        </head>
        <body onload='initMap()'>
            <div id='map'></div>
        </body>
        </html>";

                webViewMap.NavigateToString(html);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat Maps: " + ex.Message);
            }
        }


        // ================== FULLSCREEN ==================
        private void BtnFullscreen_Click(object sender, EventArgs e)
        {
            if (!isFullscreen)
            {
                originalBounds = webViewMap.Bounds;

                webViewMap.Dock = DockStyle.Fill;
                webViewMap.BringToFront();

                btnFullscreen.Text = "Kecilkan";
                isFullscreen = true;
            }
            else
            {
                webViewMap.Dock = DockStyle.None;
                webViewMap.Bounds = originalBounds;

                btnFullscreen.Text = "Fullscreen";
                isFullscreen = false;
            }
        }

        // ================== DATA DUMMY ==================
        private void LoadDummyData()
        {
            listSampah = new()
            {
                new Sampah { Id = "1", Nama = "Plastik", BeratKg = 5, Lokasi = "Jakarta" },
                new Sampah { Id = "2", Nama = "Kertas", BeratKg = 3, Lokasi = "Bandung" }
            };

            listPetugas = new()
            {
                new User { Id = "1", Username = "admin", Role = "Admin" },
                new User { Id = "2", Username = "petugas1", Role = "Petugas" }
            };

            cboSampah.Items.Clear();
            foreach (var s in listSampah)
                cboSampah.Items.Add($"{s.Nama} ({s.BeratKg} kg)");

            cboPetugas.Items.Clear();
            foreach (var p in listPetugas)
                cboPetugas.Items.Add(p.Username);

            RefreshGrid();
        }

        // ================== AUTO ZOOM ==================
        private void CboSampah_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSampah.SelectedIndex < 0) return;

            var lokasi = listSampah[cboSampah.SelectedIndex].Lokasi;

            if (webViewMap.CoreWebView2 != null)
            {
                webViewMap.Source =
                    new Uri($"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString(lokasi)}");
            }
        }

        // ================== SIMPAN ==================
        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (cboSampah.SelectedIndex < 0 || cboPetugas.SelectedIndex < 0)
            {
                MessageBox.Show("Lengkapi data!");
                return;
            }

            var item = new Penjemputan
            {
                Id = string.IsNullOrEmpty(selectedId) ? Guid.NewGuid().ToString() : selectedId,
                SampahID = listSampah[cboSampah.SelectedIndex].Id,
                PetugasID = listPetugas[cboPetugas.SelectedIndex].Id,
                TanggalJadwal = dtpTanggalJadwal.Value,
                Status = cboStatus.SelectedItem?.ToString(),
                Catatan = txtCatatan.Text,
                NamaSampah = listSampah[cboSampah.SelectedIndex].Nama,
                NamaPetugas = listPetugas[cboPetugas.SelectedIndex].Username
            };

            if (string.IsNullOrEmpty(selectedId))
                listPenjemputan.Add(item);
            else
            {
                var index = listPenjemputan.FindIndex(x => x.Id == selectedId);
                if (index >= 0) listPenjemputan[index] = item;
            }

            RefreshGrid();
            ClearInputs();
            MessageBox.Show("Berhasil disimpan (mode offline)");
        }

        // ================== HAPUS ==================
        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedId)) return;

            listPenjemputan.RemoveAll(x => x.Id == selectedId);
            RefreshGrid();
            ClearInputs();
        }

        // ================== GRID CLICK ==================
        private void DgvPenjemputan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var item = dgvPenjemputan.Rows[e.RowIndex].DataBoundItem as Penjemputan;
            if (item == null) return;

            selectedId = item.Id;

            cboSampah.SelectedIndex =
                listSampah.FindIndex(x => x.Id == item.SampahID);

            cboPetugas.SelectedIndex =
                listPetugas.FindIndex(x => x.Id == item.PetugasID);

            dtpTanggalJadwal.Value = item.TanggalJadwal;
            cboStatus.SelectedItem = item.Status;
            txtCatatan.Text = item.Catatan;

            btnSimpan.Text = "Update";
        }

        private void RefreshGrid()
        {
            dgvPenjemputan.DataSource = null;
            dgvPenjemputan.DataSource = listPenjemputan;
        }

        private void ClearInputs()
        {
            selectedId = "";
            cboSampah.SelectedIndex = -1;
            cboPetugas.SelectedIndex = -1;
            txtCatatan.Clear();
            btnSimpan.Text = "Simpan";
        }
    }
}
