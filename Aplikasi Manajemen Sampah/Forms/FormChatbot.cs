using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Aplikasi_Manajemen_Sampah.Models;

namespace Aplikasi_Manajemen_Sampah.Forms
{
    public partial class FormChatbot : Form
    {
        private readonly HttpClient _httpClient;
        private List<ChatMessage> _conversationHistory;
        private const string MistralApiUrl = "https://api.mistral.ai/v1/chat/completions";
        private User currentUser;

        // Model Mistral yang tersedia
        private readonly Dictionary<string, string> _mistralModels = new Dictionary<string, string>
        {
            { "mistral-tiny", "Mistral 7B (Cepat & Hemat)" },
            { "mistral-small", "Mixtral 8x7B (Seimbang)" },
            { "mistral-medium", "Mistral Medium (Terbaik)" },
            { "open-mistral-7b", "Open Mistral 7B" },
            { "open-mixtral-8x7b", "Open Mixtral 8x7B" }
        };

        public FormChatbot(User user)
        {
            this.currentUser = user;
            InitializeComponent();
            _httpClient = new HttpClient();
            _conversationHistory = new List<ChatMessage>();
            InitializeConversation();
            InitializeModelDropdown();
            CustomizeDesign();
        }

        private void CustomizeDesign()
        {
            // Styling sesuai tema Bank Sampah
            this.BackColor = Color.FromArgb(245, 247, 250);

            if (panelHeader != null)
            {
                panelHeader.BackColor = Color.FromArgb(30, 50, 40);
                lblTitle.ForeColor = Color.White;
            }

            if (txtConversation != null)
            {
                txtConversation.BackColor = Color.White;
                txtConversation.Font = new Font("Segoe UI", 10F);
            }

            if (txtMessage != null)
            {
                txtMessage.Font = new Font("Segoe UI", 10F);
            }

            // Styling tombol
            UIHelper.SetPrimaryButton(btnSend);
            UIHelper.SetSecondaryButton(btnClear);
            UIHelper.SetSecondaryButton(btnSaveChat);
        }

        private void InitializeModelDropdown()
        {
            cmbModel.Items.Clear();
            foreach (var model in _mistralModels)
            {
                cmbModel.Items.Add($"{model.Key}");
            }
            cmbModel.SelectedIndex = 0;
        }

        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update label info model
            if (cmbModel.SelectedItem != null)
            {
                string selectedKey = cmbModel.SelectedItem.ToString();
                if (_mistralModels.ContainsKey(selectedKey))
                {
                    lblModelInfo.Text = $"Model: {_mistralModels[selectedKey]}";
                }
            }
        }

        private void InitializeConversation()
        {
            // System prompt disesuaikan untuk Bank Sampah
            _conversationHistory.Add(new ChatMessage
            {
                role = "system",
                content = $"Anda adalah asisten AI untuk Aplikasi Manajemen Bank Sampah. " +
                          $"Nama pengguna: {currentUser.Username}, Role: {currentUser.Role}. " +
                          "Gunakan bahasa Indonesia yang ramah dan mudah dipahami. " +
                          "Berikan jawaban yang informatif dan praktis."
            });

            UpdateConversationDisplay();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendMessage();
        }

        private async void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !ModifierKeys.HasFlag(Keys.Shift))
            {
                e.Handled = true;
                await SendMessage();
            }
        }

        private async Task SendMessage()
        {
            string userMessage = txtMessage.Text.Trim();

            if (string.IsNullOrEmpty(userMessage))
            {
                MessageBox.Show("Silakan ketik pesan terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtApiKey.Text))
            {
                MessageBox.Show("Masukkan Mistral API Key terlebih dahulu.\n\n" +
                               "Dapatkan API Key di: https://console.mistral.ai/",
                               "API Key Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tambahkan pesan pengguna ke history
            _conversationHistory.Add(new ChatMessage
            {
                role = "user",
                content = userMessage
            });

            UpdateConversationDisplay();

            string originalMessage = userMessage;
            txtMessage.Clear();
            txtMessage.Focus();

            try
            {
                btnSend.Enabled = false;
                btnSend.Text = "Mengirim...";

                string response = await GetMistralResponse(originalMessage);

                _conversationHistory.Add(new ChatMessage
                {
                    role = "assistant",
                    content = response
                });

                UpdateConversationDisplay();
                lblStatus.Text = "Siap";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error terjadi";

                _conversationHistory.RemoveAt(_conversationHistory.Count - 1);

                _conversationHistory.Add(new ChatMessage
                {
                    role = "assistant",
                    content = $"❌ Gagal mendapatkan respons: {ex.Message}"
                });
                UpdateConversationDisplay();
            }
            finally
            {
                btnSend.Enabled = true;
                btnSend.Text = "Kirim";
            }
        }

        private async Task<string> GetMistralResponse(string userMessage)
        {
            lblStatus.Text = "Mengirim ke Mistral AI...";

            string selectedModel = GetSelectedModel();

            var requestData = new
            {
                model = selectedModel,
                messages = _conversationHistory,
                max_tokens = 2000,
                temperature = 0.7,
                top_p = 0.9,
                stream = false
            };

            string jsonData = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {txtApiKey.Text}");

            HttpResponseMessage response = await _httpClient.PostAsync(MistralApiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);

                string errorMessage = errorResponse?.error?.message ?? "Unknown error";

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new Exception($"API Key tidak valid. Periksa kembali API Key Anda.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    throw new Exception($"Terlalu banyak request. Tunggu sebentar dan coba lagi.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new Exception($"Request tidak valid: {errorMessage}");
                }
                else
                {
                    throw new Exception($"Mistral API Error ({response.StatusCode}): {errorMessage}");
                }
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<MistralApiResponse>(responseContent);

            lblStatus.Text = "Menerima respons...";
            return apiResponse.choices[0].message.content;
        }

        private string GetSelectedModel()
        {
            if (cmbModel.SelectedItem == null)
                return "mistral-tiny";

            return cmbModel.SelectedItem.ToString();
        }

        private void UpdateConversationDisplay()
        {
            txtConversation.Clear();

            foreach (var message in _conversationHistory)
            {
                if (message.role == "system") continue;

                string prefix = message.role == "user" ? "👤 Anda: " : "🤖 Asisten: ";
                Color color = message.role == "user" ? Color.FromArgb(30, 50, 40) : Color.FromArgb(46, 204, 113);
                FontStyle style = FontStyle.Bold;

                txtConversation.SelectionColor = color;
                txtConversation.SelectionFont = new Font(txtConversation.Font, style);
                txtConversation.AppendText($"{prefix}\n");

                txtConversation.SelectionColor = Color.Black;
                txtConversation.SelectionFont = new Font(txtConversation.Font, FontStyle.Regular);
                txtConversation.AppendText($"{message.content}\n\n");
            }

            txtConversation.ScrollToCaret();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Apakah Anda yakin ingin membersihkan percakapan?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _conversationHistory.Clear();
                InitializeConversation();
                txtMessage.Clear();
                lblStatus.Text = "Percakapan dibersihkan";
            }
        }

        private void txtApiKey_TextChanged(object sender, EventArgs e)
        {
            if (txtApiKey.Text.Length > 10)
            {
                txtApiKey.BackColor = Color.LightGreen;
            }
            else
            {
                txtApiKey.BackColor = Color.White;
            }
        }

        private void btnSaveChat_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text Files (*.txt)|*.txt|JSON Files (*.json)|*.json";
            saveDialog.Title = "Simpan Percakapan";
            saveDialog.FileName = $"ChatBot_{DateTime.Now:yyyyMMdd_HHmmss}";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string content = "";

                    if (saveDialog.FilterIndex == 2)
                    {
                        content = JsonConvert.SerializeObject(_conversationHistory, Formatting.Indented);
                    }
                    else
                    {
                        content += $"=== PERCAKAPAN CHATBOT BANK SAMPAH ===\n";
                        content += $"User: {currentUser.Username} ({currentUser.Role})\n";
                        content += $"Tanggal: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n";
                        content += $"=====================================\n\n";

                        foreach (var message in _conversationHistory)
                        {
                            if (message.role == "system") continue;
                            content += $"{message.role.ToUpper()}: {message.content}\n\n";
                            content += "---\n\n";
                        }
                    }

                    System.IO.File.WriteAllText(saveDialog.FileName, content);
                    MessageBox.Show("Percakapan berhasil disimpan!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal menyimpan: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Kelas untuk struktur data
        public class ChatMessage
        {
            public string role { get; set; }
            public string content { get; set; }
        }

        public class MistralApiResponse
        {
            public string id { get; set; }
            public string @object { get; set; }
            public long created { get; set; }
            public string model { get; set; }
            public List<Choice> choices { get; set; }
            public Usage usage { get; set; }
        }

        public class Choice
        {
            public int index { get; set; }
            public ChatMessage message { get; set; }
            public string finish_reason { get; set; }
        }

        public class Usage
        {
            public int prompt_tokens { get; set; }
            public int total_tokens { get; set; }
            public int completion_tokens { get; set; }
        }

        public class ErrorResponse
        {
            public ErrorDetail error { get; set; }
        }

        public class ErrorDetail
        {
            public string message { get; set; }
            public string type { get; set; }
            public string param { get; set; }
            public string code { get; set; }
        }
    }
}