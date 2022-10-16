using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AnalizBibl
{
    public class Analiz
    {
        string[] keyWords = {"select", "case", "as", "integer", "else", "end"};
        const byte maxleng = 8;
        bool noError = true;
        string[] specsym = { ":", ";", "+","*","(",")", "="};
        //string[] commsym = { "//", "/*", "*/" };
        //string[] oneCommsym = { "/", "*" };
        string path;
        //const string path = "Data.txt";
        //static readonly string[] States = { "Idn", "Lit", "Rzd" };
        enum States
        {
            None, Idn, Lit, Rzd
        }
        
        States state = States.None;

        //string State = "";

        List<string> Idn = new List<string>();
        List<string> Lit = new List<string>();
        List<string> Rzd = new List<string>();

        string buff = "";

        private Analiz() { }
        public Analiz(string path)
        {
            this.path = path;
        }

        public bool Scaning()
        {
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
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
            return noError;
        }
        public List<string> ReturnIdent()
        {
            return Idn;
        }
        public List<string> ReturnLiter()
        {
            return Lit;
        }
        public List<string> ReturnRzd()
        {
            return Rzd;
        }
        public List<string> ReturnKeyWords()
        {
            List<string> kw = new List<string>();
            foreach(string s in keyWords)
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

        //private void CheckLine(string line)
        //{

        //    CheckInd(line);
        //    CheckLit(line);
        //    CheckRzd(line);


        //}
        //private void CheckInd(string line)
        //{
            
        //}
        //private void CheckLit(string line)
        //{

        //}
        //private void CheckRzd(string line)
        //{

        //}
        //private bool CheckCommentOne(string s)
        //{
        //    bool b = false;
        //    foreach(string st in oneCommsym)
        //    {
        //        if(st == s)
        //        {
        //            b = true;
        //            break;
        //        }
        //    }
        //    return b;
        //}
        //private bool CheckComment(string s)
        //{
        //    bool b = false;
        //    foreach (string st in commsym)
        //    {
        //        if (st == s)
        //        {
        //            b = true;
        //            break;
        //        }
        //    }
        //    return b;
        //}
        //private bool CheckEndComment(string s)
        //{
        //    bool b = false;
        //    if(s == "\n")
        //    {
        //        b = true;
        //    }

        //    return b;
        //}
        private bool CheckSpase(string s)
        {
            Regex regex = new Regex(@"\s");
            bool b = false;

            if (regex.IsMatch(s))
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
                
                //if (CheckCommentOne(sym))
                //{
                //    string buff2 = sym + (char)reader.Peek();
                //    if (CheckComment(buff2))
                //    {
                //        do
                //        {
                //            sym = ((char)reader.Read()).ToString();
                //        } while (!CheckEndComment(sym));
                //    }
                //}

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
                           // if (!Lit.Contains(buff))
                           // {
                                Lit.Add(buff);
                            //}
                            break;
                        }
                    case States.Rzd:
                        {
                           // if (!Rzd.Contains(buff))
                            //{
                                Rzd.Add(buff);
                           // }
                            break;
                        }
                    case States.Idn:
                        {
                           // if (!Idn.Contains(buff))
                           // {
                                Idn.Add(buff);
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

            //if (CheckCommentOne(sym))
            //{

            //    string buff2 = sym + (char)reader.Peek();
            //    if (CheckComment(buff2))
            //    {
            //        do
            //        {
            //            sym = ((char)reader.Read()).ToString();
            //        } while (!CheckEndComment(sym));
            //    }
            //}

            switch (state){
                case States.Lit:
                    {
                        if (!(IsDigit(sym) || IsSpecSym(sym)))
                        {
                            noError = false;
                            buff = "";
                            return;
                        }
                        else if (IsSpecSym(sym))
                        {
                           // if (!Lit.Contains(buff))
                           // {
                                Lit.Add(buff);
                            //}                           //buff or buff2?
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
                            Rzd.Add(buff);
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
                                Idn.Add(buff);
                           // }                                //buff or buff2?
                            buff = "";
                            state= States.None;
                            ReadSym(sym);
                        }
                        else
                        {
                            if (buff.Length >= maxleng)
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
