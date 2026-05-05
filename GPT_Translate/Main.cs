using System;
using System.Diagnostics;

namespace GPT_Translate
{
    public partial class Main : Form
    {
        private readonly GeminiHelper _helper;
        private List<SubtitleItem> _subtitles = new List<SubtitleItem>();
        public Main()
        {
            InitializeComponent();
            string apiKey = Helper.Decrypt(Constants.GoogleApiKey);
            _helper = new GeminiHelper(apiKey);
        }

        private async void buttonTranslate_ClickAsync(object sender, EventArgs e)
        {
            _subtitles = new List<SubtitleItem>();
            richTextBoxOutput.Clear();
            // convert to list of subtitle items
            if (string.IsNullOrWhiteSpace(richTextBoxInput.Text))
                return;
            _subtitles = Helper.ParseSubtitle(richTextBoxInput.Text);
            if (_subtitles.Count == 0)
            {
                MessageBox.Show("No valid subtitle items found. Please check the input format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string textToTranslate = "";// string.Join("|", _subtitles.Select(s => s.Text));

            string defaultChat = $"Dịch sang tiếng việt, chỉ dịch và giữ nguyên format giữ nguyên dấu | và không giải thích gì thêm: ";
            int batchSize = 0; // Số lượng subtitle mỗi batch
            for (int i = 0; i < _subtitles.Count; i++)
            {
                textToTranslate += _subtitles[i].Text;
                batchSize++;
                if (i < _subtitles.Count - 1)
                    textToTranslate += " | ";
                if (batchSize >= 10 || i == _subtitles.Count - 1) // Gửi mỗi batch sau khi đạt đến batchSize hoặc khi là batch cuối cùng
                {
                    string inputChat = defaultChat + textToTranslate;
                    var result = await _helper.SendMessageAsync(inputChat);
                    string[] translatedTexts = result.Split('|');
                    for (int j = 0; j < batchSize; j++)
                    {
                        int index = i - batchSize + 1 + j;
                        if (index < _subtitles.Count && j < translatedTexts.Length)
                        {
                            _subtitles[index].Text = translatedTexts[j].Trim();
                            richTextBoxOutput.AppendText($"{_subtitles[index].No}\n{_subtitles[index].Time}\n{_subtitles[index].Text}\n\n");
                        }
                    }
                    // Reset cho batch tiếp theo
                    textToTranslate = "";
                    batchSize = 0;
                    // add delay 1s
                    await Task.Delay(2500);
                }
            }
        }
        private async Task OpenAITranslateAsync(string input)
        {
            var helper = new OpenAIHelper(Constants.ApiKey);
            if (string.IsNullOrWhiteSpace(richTextBoxInput.Text))
                return;

            buttonTranslate.Enabled = false;
            richTextBoxOutput.AppendText("You: " + richTextBoxInput.Text + Environment.NewLine);

            try
            {
                var result = await _helper.SendMessageAsync(richTextBoxInput.Text);

                richTextBoxOutput.AppendText("AI: " + result + Environment.NewLine);
                richTextBoxOutput.AppendText("-----------------------------\n");
            }
            catch (Exception ex)
            {
                richTextBoxOutput.AppendText("Error: " + ex.Message + Environment.NewLine);
            }

            richTextBoxInput.Clear();
            buttonTranslate.Enabled = true;
        }

        private void buttonEncrypted_Click(object sender, EventArgs e)
        {
            richTextBoxOutput.Text = Helper.Encrypt(richTextBoxInput.Text);

        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            richTextBoxOutput.Text = Helper.Decrypt(richTextBoxInput.Text);
        }
    }
}
