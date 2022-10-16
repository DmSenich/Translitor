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

        
        List<string> Idn = new List<string>();
        List<string> Lit = new List<string>();
        List<string> Rzd = new List<string>();
        List<string> keyWords;

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
                List<string> allIdn = analiz.ReturnIdent();
                List<string> allLit = analiz.ReturnLiter();
                List<string> allRzd = analiz.ReturnRzd();

                keyWords = analiz.ReturnKeyWords();
                foreach(string s in allIdn)
                {
                    if (!Idn.Contains(s))
                    {
                        Idn.Add(s);
                    }
                    dataGridView1.Rows.Add(s, "Идентификатор");
                }
                foreach(string s in allLit)
                {
                    if (!Lit.Contains(s))
                    {
                        Lit.Add(s);
                    }
                    dataGridView1.Rows.Add(s, "Литерал");
                }
                foreach (string s in allRzd)
                {
                    if (!Rzd.Contains(s))
                    {
                        Rzd.Add(s);
                    }
                    dataGridView1.Rows.Add(s, "Разделитель");
                }
            }
            else
            {
                MessageBox.Show("Несуществующий файл или лексические ошибки!", "Ошибка");
            }
        }

        private void bCrTb_Click(object sender, EventArgs e)
        {
            try
            {
                Form2 form2 = new Form2(Idn, Lit, Rzd, keyWords);
                form2.Show();
            }
            catch
            {
                MessageBox.Show("Загрузите файл", "Ошибка");
            }
        }
    }
}
