using System.Windows.Forms;
using System.Drawing;

namespace Aplikasi_Manajemen_Sampah.Forms
{
    partial class FormChatbot
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelHeader;
        private Label lblTitle;
        private RichTextBox txtConversation;
        private TextBox txtMessage;
        private Button btnSend;
        private Button btnClear;
        private Button btnSaveChat;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private Panel panelApiKey;
        private TextBox txtApiKey;
        private Label lblApiKey;
        private ComboBox cmbModel;
        private Label lblModel;
        private Label lblModelInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitle = new Label();
            txtConversation = new RichTextBox();
            txtMessage = new TextBox();
            btnSend = new Button();
            btnClear = new Button();
            btnSaveChat = new Button();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            panelApiKey = new Panel();
            lblModelInfo = new Label();
            cmbModel = new ComboBox();
            lblModel = new Label();
            txtApiKey = new TextBox();
            lblApiKey = new Label();
            panelHeader.SuspendLayout();
            statusStrip1.SuspendLayout();
            panelApiKey.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(30, 50, 40);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 4, 3, 4);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1029, 80);
            panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1029, 80);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🤖 Asisten Bank Sampah";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtConversation
            // 
            txtConversation.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtConversation.BackColor = Color.White;
            txtConversation.BorderStyle = BorderStyle.None;
            txtConversation.Font = new Font("Segoe UI", 10F);
            txtConversation.Location = new Point(17, 187);
            txtConversation.Margin = new Padding(3, 4, 3, 4);
            txtConversation.Name = "txtConversation";
            txtConversation.ReadOnly = true;
            txtConversation.Size = new Size(994, 453);
            txtConversation.TabIndex = 2;
            txtConversation.Text = "";
            // 
            // txtMessage
            // 
            txtMessage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtMessage.Font = new Font("Segoe UI", 10F);
            txtMessage.Location = new Point(17, 653);
            txtMessage.Margin = new Padding(3, 4, 3, 4);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.PlaceholderText = "Ketik pesan Anda di sini...";
            txtMessage.Size = new Size(868, 92);
            txtMessage.TabIndex = 3;
            txtMessage.KeyPress += txtMessage_KeyPress;
            // 
            // btnSend
            // 
            btnSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSend.BackColor = Color.FromArgb(46, 204, 113);
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSend.ForeColor = Color.White;
            btnSend.Location = new Point(897, 653);
            btnSend.Margin = new Padding(3, 4, 3, 4);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(114, 93);
            btnSend.TabIndex = 4;
            btnSend.Text = "Kirim";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnClear.BackColor = Color.FromArgb(231, 76, 60);
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(17, 760);
            btnClear.Margin = new Padding(3, 4, 3, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(114, 47);
            btnClear.TabIndex = 5;
            btnClear.Text = "🗑️ Bersihkan";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnSaveChat
            // 
            btnSaveChat.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSaveChat.BackColor = Color.FromArgb(52, 152, 219);
            btnSaveChat.FlatAppearance.BorderSize = 0;
            btnSaveChat.FlatStyle = FlatStyle.Flat;
            btnSaveChat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSaveChat.ForeColor = Color.White;
            btnSaveChat.Location = new Point(143, 760);
            btnSaveChat.Margin = new Padding(3, 4, 3, 4);
            btnSaveChat.Name = "btnSaveChat";
            btnSaveChat.Size = new Size(114, 47);
            btnSaveChat.TabIndex = 6;
            btnSaveChat.Text = "💾 Simpan";
            btnSaveChat.UseVisualStyleBackColor = false;
            btnSaveChat.Click += btnSaveChat_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip1.Location = new Point(0, 823);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 16, 0);
            statusStrip1.Size = new Size(1029, 26);
            statusStrip1.TabIndex = 7;
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(38, 20);
            lblStatus.Text = "Siap";
            // 
            // panelApiKey
            // 
            panelApiKey.BackColor = Color.White;
            panelApiKey.Controls.Add(lblModelInfo);
            panelApiKey.Controls.Add(cmbModel);
            panelApiKey.Controls.Add(lblModel);
            panelApiKey.Controls.Add(txtApiKey);
            panelApiKey.Controls.Add(lblApiKey);
            panelApiKey.Dock = DockStyle.Top;
            panelApiKey.Location = new Point(0, 80);
            panelApiKey.Margin = new Padding(3, 4, 3, 4);
            panelApiKey.Name = "panelApiKey";
            panelApiKey.Padding = new Padding(17, 13, 17, 13);
            panelApiKey.Size = new Size(1029, 93);
            panelApiKey.TabIndex = 1;
            // 
            // lblModelInfo
            // 
            lblModelInfo.AutoSize = true;
            lblModelInfo.Font = new Font("Segoe UI", 8F);
            lblModelInfo.ForeColor = Color.Gray;
            lblModelInfo.Location = new Point(623, 51);
            lblModelInfo.Name = "lblModelInfo";
            lblModelInfo.Size = new Size(117, 19);
            lblModelInfo.TabIndex = 4;
            lblModelInfo.Text = "Model: Mistral 7B";
            // 
            // cmbModel
            // 
            cmbModel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModel.Font = new Font("Segoe UI", 9F);
            cmbModel.FormattingEnabled = true;
            cmbModel.Location = new Point(377, 47);
            cmbModel.Margin = new Padding(3, 4, 3, 4);
            cmbModel.Name = "cmbModel";
            cmbModel.Size = new Size(228, 28);
            cmbModel.TabIndex = 3;
            cmbModel.SelectedIndexChanged += cmbModel_SelectedIndexChanged;
            // 
            // lblModel
            // 
            lblModel.AutoSize = true;
            lblModel.Font = new Font("Segoe UI", 9F);
            lblModel.Location = new Point(377, 20);
            lblModel.Name = "lblModel";
            lblModel.Size = new Size(55, 20);
            lblModel.TabIndex = 2;
            lblModel.Text = "Model:";
            // 
            // txtApiKey
            // 
            txtApiKey.Font = new Font("Segoe UI", 9F);
            txtApiKey.Location = new Point(17, 47);
            txtApiKey.Margin = new Padding(3, 4, 3, 4);
            txtApiKey.Name = "txtApiKey";
            txtApiKey.PasswordChar = '●';
            txtApiKey.Size = new Size(342, 27);
            txtApiKey.TabIndex = 1;
            txtApiKey.TextChanged += txtApiKey_TextChanged;
            // 
            // lblApiKey
            // 
            lblApiKey.AutoSize = true;
            lblApiKey.Font = new Font("Segoe UI", 9F);
            lblApiKey.Location = new Point(17, 20);
            lblApiKey.Name = "lblApiKey";
            lblApiKey.Size = new Size(62, 20);
            lblApiKey.TabIndex = 0;
            lblApiKey.Text = "API Key:";
            // 
            // FormChatbot
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1029, 849);
            Controls.Add(statusStrip1);
            Controls.Add(btnSaveChat);
            Controls.Add(btnClear);
            Controls.Add(btnSend);
            Controls.Add(txtMessage);
            Controls.Add(txtConversation);
            Controls.Add(panelApiKey);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(912, 784);
            Name = "FormChatbot";
            Text = "Asisten AI - Bank Sampah";
            panelHeader.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panelApiKey.ResumeLayout(false);
            panelApiKey.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}