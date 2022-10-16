using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalizBibl;

namespace Main
{
    public partial class Form1 : Form
    {
        const string path = "Data.txt";
        
        public Form1()
        {
            InitializeComponent();
        }
        
        private void bLoad_Click(object sender, EventArgs e)
        {
            
            dataGridView1.Rows.Clear();
            Analiz analiz = new Analiz(path);
            if (analiz.Scaning())
            {

                List<Token> allWords = analiz.ReturnAllWords();

                foreach(Token tok in allWords)
                {
                    dataGridView1.Rows.Add(tok.word, tok.type);
                }
            }
            else
            {
                MessageBox.Show("Несуществующий файл или лексические ошибки!", "Ошибка");
            }
        }
    }
}
