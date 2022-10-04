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
                List<string> Idn = analiz.ReturnIdent();
                List<string> Lit = analiz.ReturnLiter();
                List<string> Rzd = analiz.ReturnRzd();

                foreach(string s in Idn)
                {
                    dataGridView1.Rows.Add(s, "Идентификатор");
                }
                foreach(string s in Lit)
                {
                    dataGridView1.Rows.Add(s, "Литерал");
                }
                foreach (string s in Rzd)
                {
                    dataGridView1.Rows.Add(s, "Разделитель");
                }
            }
            else
            {
                MessageBox.Show("Несуществующий файл или лексические ошибки!", "Ошибка");
            }
        }
    }
}
