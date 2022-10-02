using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizBibl
{
    public class Analiz
    {
        const byte maxleng = 8;
        string[] specsym = { ":", ";", "+", ":=", "=" };
        const string path = "Data.txt";
        string[] States = { "Idn", "Lit", "Rzd" };
        string State = "";

        List<string> Idn = new List<string>();
        List<string> Lit = new List<string>();
        List<string> Rzd = new List<string>();

        string buff = "";

        public void Scaning()
        {
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                string sym;
                while ((sym = reader.Read().ToString()) != null)
                {
                    
                    ReadSym(sym);
                }
            }
        }
        private void ToState(string s)
        {
            foreach(string spec in specsym)
            {
                if(s == spec)
                {
                    State = States[2];
                    break;
                }
            }
            if(State == "")
            {
                if(int.TryParse(s, out int n))
                {
                    State = States[1];
                }
                else
                {
                    State = States[0];
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

        private void CheckLine(string line)
        {

            CheckInd(line);
            CheckLit(line);
            CheckRzd(line);


        }
        private void CheckInd(string line)
        {
            
        }
        private void CheckLit(string line)
        {

        }
        private void CheckRzd(string line)
        {

        }
        private void ReadSym(string sym)
        {
            string buff2 = buff;
            if(buff == "")
            {
                if (IsSpecSym(sym))
                {
                    State = States[2];
                    
                }
                else
                {
                    if (IsDigit(sym))
                    {
                        State = States[1];
                    }
                    else
                    {
                        State = States[0];
                    }
                }
                buff = sym;
                return;
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
