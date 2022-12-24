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
    public partial class FormSyntacticAnalysis : Form
    {
        //List<string> Idn;
        //List<string> Lit;
        //List<string> Rzd;
        List<Token> allWords;
        List<Token> Lit = new List<Token>();
        List<Token> Idn = new List<Token>();
        List<Token> Rzd = new List<Token>();
        List<Token> key = new List<Token>();
        //Dictionary<Token, TokenTableNum> tokenTable = new Dictionary<Token, TokenTableNum>();
        Dictionary<Token, int> tokenTable = new Dictionary<Token, int>();
        List<string> keyWords;
        DataTable dtKey = new DataTable(),
            dtI = new DataTable(),
            dtR = new DataTable(),
            dtL = new DataTable(),
            dtAll = new DataTable();

        Syntactic validate;
        string sComplite = "Разбор программы успешен";
        string sError = "Обнаружена ошибка: ";
        public FormSyntacticAnalysis()
        {
            InitializeComponent();
        }
        public FormSyntacticAnalysis(List<Token> allWords, List<string> keyWords, Dictionary<Token, int> tokenTable) : this()
        {

            this.allWords = allWords;
            this.keyWords = keyWords;
            this.tokenTable = tokenTable;
            ToCreateListing();
            ToCreateTables();

            validate = new Syntactic(allWords, key, Idn, Rzd, Lit);

            try
            {
                validate.Program();

                lRes.Text = sComplite;
                foreach (ExprClass exprClass in validate.ListMatrix)
                {
                    richTextBox1.Text += exprClass.RPN() + "\n\n";
                    richTextBox1.Text += exprClass.ToString() + "\n\n";
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                int curr = validate.Current;
                lRes.Text = sError + allWords[curr].word + " " + curr;
            }
            //if (validate.Program())
            //{
            //    lRes.Text = sComplite;
            //    richTextBox1.Text = validate.Matrix.ToString();
            //}
            //else
            //{
            //    int curr = validate.Current;
            //    lRes.Text = sError + allWords[curr].word + " " + curr;
            //}
        }
        private void ToCreateListing()
        {
            foreach(Token word in tokenTable.Keys)
            {
                switch (word.type)
                {
                    case "I":
                        if (keyWords.Contains(word.word))
                        {
                            key.Add(word);
                        }
                        else
                        {
                            Idn.Add(word);
                        }
                        break;
                    case "L":
                        Lit.Add(word);
                        break;
                    case "R":
                        Rzd.Add(word);
                        break;
                }
            }
        }
        private void ToCreateTables()
        {
            dtKey.Columns.Add("id");
            dtKey.Columns.Add("Ключевые слова");
            
            dtI.Columns.Add("id");
            dtI.Columns.Add("Идентификаторы");

            dtL.Columns.Add("id");
            dtL.Columns.Add("Литералы");

            dtR.Columns.Add("id");
            dtR.Columns.Add("Разделители");

            dtAll.Columns.Add("Данные");
            dtAll.Columns.Add("Результат");

            for(int i = 0; i < key.Count; i++)
            {
                DataRow dr = dtKey.NewRow();
                dr[0] = i;
                dr[1] = key[i].word;
                dtKey.Rows.Add(dr);
            }
            for (int i = 0; i < Idn.Count; i++)
            {
                DataRow dr = dtI.NewRow();
                dr[0] = i;
                dr[1] = Idn[i].word;
                dtI.Rows.Add(dr);
            }
            for (int i = 0; i < Rzd.Count; i++)
            {
                DataRow dr = dtR.NewRow();
                dr[0] = i;
                dr[1] = Rzd[i].word;
                dtR.Rows.Add(dr);
            }
            for (int i = 0; i < Lit.Count; i++)
            {
                DataRow dr = dtL.NewRow();
                dr[0] = i;
                dr[1] = Lit[i].word;
                dtL.Rows.Add(dr);
            }
            for (int i = 0; i < allWords.Count; i++)
            {
                DataRow dr = dtAll.NewRow();
                dr[0] = allWords[i].word;
                int numT = -1, numN = -1;
                string res;
                switch (allWords[i].type)
                {
                    case "I":
                        if (key.Contains(allWords[i]))
                        {
                            numT = 1;
                            numN = key.IndexOf(allWords[i]);
                        }
                        else
                        {
                            numT = 2;
                            numN = Idn.IndexOf(allWords[i]);
                        }
                        break;
                    case "L":
                        numT = 4;
                        numN = Lit.IndexOf(allWords[i]);
                        break;
                    case "R":
                        numT = 3;
                        numN = Rzd.IndexOf(allWords[i]);
                        break;
                }
                res = $"({numT},{numN})";

                dr[1] = res;
                dtAll.Rows.Add(dr);
            }


            dataGridView1.DataSource = dtKey;
            dataGridView2.DataSource = dtI;
            dataGridView3.DataSource = dtR;
            dataGridView4.DataSource = dtL;
            dataGridView5.DataSource = dtAll;


        }
    }
}
