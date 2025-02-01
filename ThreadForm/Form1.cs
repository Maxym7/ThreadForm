using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;

namespace ThreadForm
{
    public partial class Form1 : Form
    {

        int start = 0, end = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            myFunc(text);
        }
        public void myFunc(object data)
        {
            string info = data as string;

            Task.Run(() =>
            {
                int sentences = 0;
                int symbols = 0;
                int words = 1;
                int questions = 0;
                int okl = 0;

                foreach (var symbol in info)
                {
                    if (symbol == '.')
                    {
                        sentences++;
                    }
                    else if (symbol == ' ')
                    {
                        words++;
                    }
                    if (symbol == '?')
                    {
                        questions++;
                        sentences++;
                    }
                    if (symbol == '!')
                    {
                        okl++;
                        sentences++;
                    }
                    symbols++;
                }
                string resultText = $"Sentences: {sentences}\nSymbols: {symbols}\n\nOriginal Text:\n{info}";
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TextAnalysis.txt");
                return (sentences, symbols, words, questions, okl);
            })
            .ContinueWith(task =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    var (sentences, symbols, words, questions, okl) = task.Result;

                    ListViewItem listViewItem = new ListViewItem("Text Analysis");
                    listViewItem.SubItems.Add(sentences.ToString());
                    listViewItem.SubItems.Add(symbols.ToString());
                    listViewItem.SubItems.Add(words.ToString());
                    listViewItem.SubItems.Add(questions.ToString());
                    listViewItem.SubItems.Add(okl.ToString());
                    listView1.Items.Add(listViewItem);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
