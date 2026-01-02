using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Aplikasi_Manajemen_Sampah.Models;
using Aplikasi_Manajemen_Sampah.Services;

namespace Aplikasi_Manajemen_Sampah.Forms
{
    public partial class DashboardAdmin : Form
    {
        private User currentUser;
        private Form activeForm = null;

        public DashboardAdmin(User user)
        {
            this.currentUser = user;
            InitializeComponent();

            // Event Load (TEST KONEKSI DATABASE ONLINE)
            this.Load += DashboardAdmin_Load;

            InitializeCustomDesign();

            // Setup Navigasi Sidebar
            if (btnSampah != null)
                btnSampah.Click += (s, e) => OpenChildForm(new FormSampah(currentUser));

            if (btnPenjemputan != null)
                btnPenjemputan.Click += (s, e) => OpenChildForm(new FormPenjemputan(currentUser));

            if (btnUsers != null)
                btnUsers.Click += (s, e) => OpenChildForm(new FormUsers(currentUser));

            if (btnCetak != null)
                btnCetak.Click += btnCetakAdmin_Click;

            if (btnChatbot != null)
                btnChatbot.Click += (s, e) => OpenChildForm(new FormChatbot(currentUser));

            if (btnLogout != null)
                btnLogout.Click += BtnLogout_Click;
        }

        // =========================
        // TEST KONEKSI MONGODB ATLAS
        // =========================
        private void DashboardAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                MongoService mongo = new MongoService();
                var jumlahSampah = mongo.Sampah.CountDocuments(_ => true);

                Console.WriteLine(
                    "Koneksi MongoDB Atlas BERHASIL. Jumlah data sampah: " + jumlahSampah
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gagal koneksi database online\n" + ex.Message,
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================
        // UI & SIDEBAR
        // =========================
        private void InitializeCustomDesign()
        {
            // Set Username
            if (Controls.Find("lblWelcome", true).Length > 0)
                ((Label)Controls.Find("lblWelcome", true)[0]).Text =
                    $"Welcome, {currentUser.Username}";

            // Sidebar Buttons
            if (btnSampah != null) { UIHelper.SetSidebarButton(btnSampah); SetupButtonHover(btnSampah); }
            if (btnPenjemputan != null) { UIHelper.SetSidebarButton(btnPenjemputan); SetupButtonHover(btnPenjemputan); }
            if (btnUsers != null) { UIHelper.SetSidebarButton(btnUsers); SetupButtonHover(btnUsers); }
            if (btnCetak != null) { UIHelper.SetSidebarButton(btnCetak); SetupButtonHover(btnCetak); }
            if (btnChatbot != null) { UIHelper.SetSidebarButton(btnChatbot); SetupButtonHover(btnChatbot); }

            if (btnLogout != null)
            {
                UIHelper.SetSidebarButton(btnLogout);
                btnLogout.BackColor = Color.FromArgb(192, 57, 43);
            }

            // Hak Akses
            if (currentUser.Role != "Admin")
            {
                if (btnUsers != null)
                    btnUsers.Visible = false;
            }
        }

        private void SetupButtonHover(Button btn)
        {
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(46, 204, 113);
            btn.MouseLeave += (s, e) => btn.BackColor = UIHelper.PrimaryColor;
        }

        // =========================
        // CHILD FORM HANDLER
        // =========================
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelContent.Controls.Clear();
            panelContent.Controls.Add(childForm);

            childForm.BringToFront();
            childForm.Show();
        }

        // =========================
        // LOGOUT
        // =========================
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Apakah Anda yakin ingin logout?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new LoginForm().Show();
                this.Hide();
            }
        }

        // =========================
        // CETAK LAPORAN
        // =========================
        private void btnCetakAdmin_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormLaporan());
        }
    }
}
