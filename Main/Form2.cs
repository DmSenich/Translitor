using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Form2 : Form
    {
        List<string> Idn;
        List<string> Lit;
        List<string> Rzd;
        List<string> keyWords;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(List<string> Idn, List<string> Lit, List<string> Rzd, List<string> keyWords) : this()
        {
            this.Idn = Idn;
            this.Lit = Lit;
            this.Rzd = Rzd;
            this.keyWords = keyWords;
            ToCreateTables();
        }

        private void ToCreateTables()
        {
            for(int i = 0; i < keyWords.Count; i++)
            {
                dataGridView1.Rows.Add(keyWords[i]);
                dataGridView5.Rows.Add(keyWords[i], $"(1, {i})");
            }
            for (int i = 0; i < Idn.Count; i++)
            {
                dataGridView2.Rows.Add(Idn[i]);
                dataGridView5.Rows.Add(Idn[i], $"(2, {i})");
            }
            for (int i = 0; i < Rzd.Count; i++)
            {
                dataGridView3.Rows.Add(Rzd[i]);
                dataGridView5.Rows.Add(Rzd[i], $"(3, {i})");
            }
            for (int i = 0; i < Lit.Count; i++)
            {
                dataGridView4.Rows.Add(Lit[i]);
                dataGridView5.Rows.Add(Lit[i], $"(4, {i})");
            }


        }
    }
}
