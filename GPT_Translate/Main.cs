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
            _helper = new GeminiHelper(Helper.Decrypt(Constants.GoogleApiKey));
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
            string textToTranslate = string.Join("|", _subtitles.Select(s => s.Text));

            string inputChat = $"Dịch sang tiếng việt, chỉ dịch và giữ nguyên format giữ nguyên dấu | và không giải thích gì thêm: " + textToTranslate;


            var result = await _helper.SendMessageAsync(inputChat);
            string[] translatedTexts = result.Split('|');
            for (int i = 0; i < _subtitles.Count; i++)
            {
                if (i < translatedTexts.Length)
                {
                    _subtitles[i].Text = translatedTexts[i].Trim();

                }
                richTextBoxOutput.AppendText($"{_subtitles[i].No}\n{_subtitles[i].Time}\n{_subtitles[i].Text}\n\n");
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
