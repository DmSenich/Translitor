using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalizBibl;

namespace Main
{
    public partial class FormLexicalAnalysis : Form
    {
        const string path = "Data.txt";
        List<Token> allWords;
        List<string> keyWords;
        //Dictionary<Token, TokenTableNum> tokenTable;
        Dictionary<Token, int> tokenTable;
        public FormLexicalAnalysis()
        {
            InitializeComponent();
        }
        
        private void bLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files(*.txt)|*.TXT|All files(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using(StreamReader reader = new StreamReader(openFileDialog1.FileName))
                    {
                        richTextBox1.Text = reader.ReadToEnd();
                        reader.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bCrTb_Click(object sender, EventArgs e)
        {
            try
            {
                FormSyntacticAnalysis form2 = new FormSyntacticAnalysis(allWords, keyWords, tokenTable);
                form2.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bOperate_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            MemoryStream memory = new MemoryStream(Encoding.ASCII.GetBytes(richTextBox1.Text));
            Lexical analiz = new Lexical(memory);

            try
            {
                analiz.Scaning();

                allWords = analiz.ReturnAllWords();
                keyWords = analiz.ReturnKeyWords();
                tokenTable = analiz.ReturnTables();

                foreach (Token tok in allWords)
                {
                    dataGridView1.Rows.Add(tok.word, tok.type);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            //if (analiz.Scaning())
            //{
            //    allWords = analiz.ReturnAllWords();
            //    keyWords = analiz.ReturnKeyWords();
            //    tokenTable = analiz.ReturnTables();

            //    foreach (Token tok in allWords)
            //    {
            //        dataGridView1.Rows.Add(tok.word, tok.type);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Несуществующий файл или лексические ошибки!", "Ошибка");
            //}
        }
    }
}
