﻿using System;
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
    public partial class Form2 : Form
    {
        //List<string> Idn;
        //List<string> Lit;
        //List<string> Rzd;
        List<Token> allWords;
        List<Token> Lit = new List<Token>();
        List<Token> Idn = new List<Token>();
        List<Token> Rzd = new List<Token>();
        List<Token> key = new List<Token>();
        Dictionary<Token, TokenTableNum> tokenTable = new Dictionary<Token, TokenTableNum>();
        List<string> keyWords;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(List<Token> allWords, List<string> keyWords, Dictionary<Token, TokenTableNum> tokenTable) : this()
        {

            this.allWords = allWords;
            this.keyWords = keyWords;
            this.tokenTable = tokenTable;
            ToCreateListing();
            ToCreateTables();
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
            foreach (Token token in key)
            {
                dataGridView1.Rows.Add(token.word);
            }
            foreach (Token token in Idn)
            {
                dataGridView2.Rows.Add(token.word);

            }
            foreach (Token token in Rzd)
            {
                dataGridView3.Rows.Add(token.word);
            }
            foreach (Token token in Lit)
            {
                dataGridView4.Rows.Add(token.word);
            }

            foreach (Token word in allWords)
            {
                
                dataGridView5.Rows.Add(word.word, $"({tokenTable[word].table}, {tokenTable[word].num})");
            }

            

            //for(int countKey = 0, countI = 0, countL = 0, countR = 0, i = 0; i<allWords.Count; i++)
            //{
            //    switch (allWords[i].type)
            //    {
            //        case "I":
            //            if (keyWords.Contains(allWords[i].word))
            //            {
            //                dataGridView1.Rows.Add(allWords[i].word);
            //                dataGridView5.Rows.Add(allWords[i].word, $"(1, {countKey})");
            //                countKey++;
            //            }
            //            else
            //            {
            //                dataGridView2.Rows.Add(allWords[i].word);
            //                dataGridView5.Rows.Add(allWords[i].word, $"(2, {countI})");
            //                countI++;
            //            }
            //            break;
            //        case "L":
            //            dataGridView4.Rows.Add(allWords[i].word);
            //            dataGridView5.Rows.Add(allWords[i].word, $"(4, {countL})");
            //            countL++;
            //            break;
            //        case "R":
            //            dataGridView3.Rows.Add(allWords[i].word);
            //            dataGridView5.Rows.Add(allWords[i].word, $"(3, {countR})");
            //            countR++;
            //            break;
            //    }
            //}


            //for(int i = 0; i < keyWords.Count; i++)
            //{
            //    dataGridView1.Rows.Add(keyWords[i]);
            //    dataGridView5.Rows.Add(keyWords[i], $"(1, {i})");
            //}
            //for (int i = 0; i < Idn.Count; i++)
            //{
            //    dataGridView2.Rows.Add(Idn[i]);
            //    dataGridView5.Rows.Add(Idn[i], $"(2, {i})");
            //}
            //for (int i = 0; i < Rzd.Count; i++)
            //{
            //    dataGridView3.Rows.Add(Rzd[i]);
            //    dataGridView5.Rows.Add(Rzd[i], $"(3, {i})");
            //}
            //for (int i = 0; i < Lit.Count; i++)
            //{
            //    dataGridView4.Rows.Add(Lit[i]);
            //    dataGridView5.Rows.Add(Lit[i], $"(4, {i})");
            //}


        }
    }
}
