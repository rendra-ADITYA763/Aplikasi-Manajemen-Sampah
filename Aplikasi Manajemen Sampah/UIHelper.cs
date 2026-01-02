using System.Drawing;
using System.Windows.Forms;

namespace Aplikasi_Manajemen_Sampah
{
    public static class UIHelper
    {
        // Palet Warna Konsisten
        public static Color PrimaryColor = Color.FromArgb(30, 50, 40);    // Hijau Sidebar
        public static Color AccentColor = Color.FromArgb(46, 204, 113);   // Hijau Tombol
        public static Color BackgroundColor = Color.FromArgb(245, 247, 250); // Abu Konten
        public static Color DangerColor = Color.FromArgb(231, 76, 60);    // Merah
        public static Color InfoColor = Color.FromArgb(52, 152, 219);     // Biru

        public static void SetGridStyle(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = AccentColor;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
            dgv.RowTemplate.Height = 35;
        }

        public static void SetSidebarButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
            btn.Dock = DockStyle.Top;
            btn.Height = 50;
        }

        // METHOD BARU: Untuk tombol hijau (primary action)
        public static void SetPrimaryButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = AccentColor;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;

            // Hover effect
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(39, 174, 96);
            btn.MouseLeave += (s, e) => btn.BackColor = AccentColor;
        }

        // METHOD BARU: Untuk tombol merah (danger/delete)
        public static void SetDangerButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = DangerColor;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;

            // Hover effect
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(192, 57, 43);
            btn.MouseLeave += (s, e) => btn.BackColor = DangerColor;
        }

        // METHOD BARU: Untuk tombol biru (info/secondary)
        public static void SetSecondaryButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = InfoColor;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;

            // Hover effect
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(41, 128, 185);
            btn.MouseLeave += (s, e) => btn.BackColor = InfoColor;
        }
    }
}