using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AnalizBibl
{
    public struct Token
    {
        public string word;
        public string type;
        public Token(string word, string type)
        {
            this.word = word;
            this.type = type;
        }
        
    }
    //public struct TokenTableNum
    //{
    //    public int table;
    //    public int num;
    //    public TokenTableNum(int table, int num)
    //    {
    //        this.table = table;
    //        this.num = num;
    //    }
    //}
    public class Lexical
    {
        const byte maxleng = 8;
        bool noError = true;
        string[] specsym = { "/", ";", "+", "-", "*", "(", ")", "=", ",", "\n" };
        string[] keyWords = { "Dim","select", "case", "as", "to", "integer" , "float", "double", "else", "end"};
        Dictionary<Token, int> tokenTable = new Dictionary<Token, int>();
        //Dictionary<Token, TokenTableNum> tokenTable = new Dictionary<Token, TokenTableNum>();
        //string[] commsym = { "//", "/*", "*/" };
        //string[] oneCommsym = { "/", "*" };
        //string path;
        //const string path = "Data.txt";
        //static readonly string[] States = { "Idn", "Lit", "Rzd" };
        enum States
        {
            None, Idn, Lit, Rzd
        }
        States state = States.None;
        MemoryStream mProgram;
        //string State = "";

        List<Token> allWords = new List<Token>();
        string buff = "";

        private Lexical() { }
        //public Analiz(string path)
        //{
        //    this.path = path;
        //}
        public Lexical(MemoryStream mProgram)
        {
            this.mProgram = mProgram;
        }
        private void ToCreateTables()
        {
            int countKey = 0, countI = 0, countL = 0, countR = 0;
            foreach (Token word in allWords)
            {
                switch (word.type)
                {
                    case "I":
                        if (keyWords.Contains(word.word))
                        {
                            if (!tokenTable.ContainsKey(word))
                            {
                                //TokenTableNum tokenNum = new TokenTableNum(1, countKey);

                                tokenTable.Add(word, countKey);
                                countKey++;
                            } 
                        }
                        else
                        {
                            if (!tokenTable.ContainsKey(word))
                            {
                                //TokenTableNum tokenNum = new TokenTableNum(2, countI);
                                tokenTable.Add(word, countI);
                                countI++;
                            }
                        }
                        break;
                    case "L":
                        if (!tokenTable.ContainsKey(word))
                        {
                            //TokenTableNum tokenNum = new TokenTableNum(4, countR);
                            tokenTable.Add(word, countR);
                            countR++;
                        }
                            
                        break;
                    case "R":
                        if (!tokenTable.ContainsKey(word))
                        {
                            //TokenTableNum tokenNum = new TokenTableNum(3, countL);
                            tokenTable.Add(word, countL);
                            countL++;
                        }
                            
                        break;
                }
            }
        }
        public void Scaning()
        {
            if (mProgram != null)
            {
                using (StreamReader reader = new StreamReader(mProgram))
                {
                    //string sym;
                    ReadSym(reader);
                    //while ((sym = reader.Read().ToString()) != null)
                    //{

                    //    ReadSym(sym);
                    //}
                    reader.Close();
                }
            }
            else
            {
                noError = false;
            }
            if (noError)
            {
                ToCreateTables();
            }
            else
            {
                throw new Exception("Обнаружена лексическая ошибка!");
            }
        }
 
        public List<Token> ReturnAllWords()

        {
            return allWords;
        }

        public Dictionary<Token, int> ReturnTables()
        {
            return tokenTable;
        }
        public List<string> ReturnKeyWords()
        {
            List<string> kw = new List<string>();
            foreach (string s in keyWords)
            {
                kw.Add(s);
            }
            return kw;

        }
        private void ToState(string s)
        {

            if (IsSpecSym(s))
            {
                //State = States[2];
                state = States.Rzd;
            }
            else if (state == States.None)
            {
                if(s == ".")
                {
                    noError = false;
                    return;
                }
                if(IsDigit(s))
                {
                    //State = States[1];
                    state = States.Lit;
                }
                else
                {
                    //State = States[0];
                    state = States.Idn;
                }
            }
            else { noError = false; return; }
        }
        private bool IsSpecSym(string s)
        {
            bool b = false;
            foreach(string spec in specsym)
            {
                if(s == spec)
                {
                    b = true;
                    break;
                }
            }
            return b;
        }
        private bool IsDigit(string s)
        {
            bool b = false;
            if(int.TryParse(s,out int n))
            {
                b = true;
            }
            return b;
        }

        private bool CheckSpase(string s)
        {
            Regex regex = new Regex(@"\s");
            bool b = false;

            if (regex.IsMatch(s) && s != "\n")
            {
                b = true;
            }
            return b;
        }
        private void ReadSym(StreamReader reader)
        {
            if (reader.Peek() != -1)
            {
                string sym = ((char)reader.Read()).ToString();

                while (CheckSpase(sym))
                {
                    if (reader.Peek() != -1)
                    {
                        sym = ((char)reader.Read()).ToString();
                    }
                    else
                    {
                        return;
                    }
                }
                

                ToState(sym);
                buff = sym;
                
                if (reader.Peek() == -1)
                {
                    ReadSym(" ");
                }


                while (reader.Peek() != -1)
                {
                    sym = ((char)reader.Read()).ToString();
                    ReadSym(sym);
                    if(!noError)
                    {
                        return;
                    }
                }
                ReadSym(" ");

            }
        }
        private void ReadSym(string sym)
        {
            if (CheckSpase(sym))
            {
                switch (state)
                {
                    case States.Lit:
                        {
                            if (buff.Last().ToString() == ".")
                            {
                                buff = buff.Remove(buff.LastIndexOf('.'));
                            }
                            // if (!Lit.Contains(buff))
                            // {
                            //Lit.Add(buff);

                            Token token = new Token(buff, "L");

                            allWords.Add(token);
                            //}
                            break;
                        }
                    case States.Rzd:
                        {
                            // if (!Rzd.Contains(buff))
                            //{

                            //Rzd.Add(buff);
                            if(buff == "\n")
                            {
                                buff = "\\n";
                            }

                            Token token = new Token(buff, "R");
                            allWords.Add(token);
                            // }
                            break;
                        }
                    case States.Idn:
                        {

                            if(buff.Last().ToString() == ".")
                            {
                                noError = false;
                                return;
                            }

                            Token token = new Token(buff, "I");
                            allWords.Add(token);
                            // }
                            break;
                        }
                }
                state = States.None;
                buff = "";
                return;
            }

            if(buff == "")
            {
                ToState(sym);
                buff = sym;
                return;
            }

            string buff2 = buff;

            switch (state){
                case States.Lit:
                    {
                        if (!(IsDigit(sym) || IsSpecSym(sym) || sym == "."))
                        {
                            noError = false;
                            buff = "";
                            return;
                        }
                        else if (IsSpecSym(sym))
                        {
                            
                            Token token = new Token(buff, "L");
                            allWords.Add(token);
                            buff = "";
                            state = States.None;
                            ReadSym(sym);
                        }
                        else
                        {
                            buff += sym;
                        }
                        break;
                    }
                case States.Rzd:
                    {
                        // if (!Rzd.Contains(buff))
                        //{

                        //Rzd.Add(buff);
                        if(buff == "\n")
                        {
                            buff = "\\n";
                        }
                        Token token = new Token(buff, "R");
                        allWords.Add(token);
                        //}                        //buff or buff2?
                        buff = "";
                        state = States.None;
                        ReadSym(sym);

                        break;
                    }
                case States.Idn:
                    {
                        if (IsSpecSym(sym))
                        {

                            // if (!Idn.Contains(buff))
                            // {
                            //Idn.Add(buff);

                            Token token = new Token(buff, "I");
                            allWords.Add(token);
                            // }                                //buff or buff2?
                            buff = "";
                            state = States.None;
                            ReadSym(sym);
                        }
                        else
                        {
                            if (buff.Length >= maxleng || sym == ".")
                            {
                                noError = false;
                                return;
                            }
                            buff += sym;
                        }
                        break;
                    }
            }




            //string buff2 = buff;

            //if (IsSpecSym(sym))
            //{
            //    CheckLine(buff2);
            //    buff = sym;
            //    State = States[2];
            //}
            ////else
            ////{
            ////    if (IsDigit(sym))
            ////    {
            ////        if(State == States[2])
            ////        {

            ////        }
            ////    }
            ////}

            //if (sym != " ")
            //{
            //    //ToState(sym);

            //    buff2 += sym;
            //}
            ////else
            ////{
            ////    buff = "";
            ////}
            //if (buff2 != "")
            //{
            //    CheckLine(buff2);
            //}
        }

    }
}
