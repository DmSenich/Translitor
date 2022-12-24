using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizBibl
{
    public class Syntactic
    {
        List<Token> allWords;

        List<Token> Lit;
        List<Token> Idn;
        List<Token> Rzd;
        List<Token> key;

        int current = 0;

        List<ExprClass> listMatrix = new List<ExprClass>();
        //ExprClass matrix;

        public Syntactic(List<Token> allWords,List<Token> key, List<Token> Idn, List<Token> Rzd, List<Token> Lit)
        {
            this.allWords = allWords;
            this.key = key;
            this.Idn = Idn;
            this.Lit = Lit;
            this.Rzd = Rzd;
        }

        public void Program()
        {
            bool res = true;

            switch (allWords[current].word)
            {
                case "Dim":
                    res = ListOfOperators();
                    break;
                //case "select":
                //    break;
                //case "id":
                //    break;
                default:
                    throw new Exception("Ожидался оператор Dim! Необходимо объявление переменных.Pr");
                    //break;
            }
            
        }
        private bool ListOfOperators()
        {
            bool res = true;

            if (!Operator())
            {
                return false;
            }

            if(current >= allWords.Count - 1)
            {
                return res;
            }

            if(allWords[current].word == "\\n")
            {
                current++;
            }
            else
            {
                throw new Exception("Ожидался переход на новую строку. ListOp");
                //return false;
            }
            if (SpawnOperator())
            {
                //current++;
            }
            else
            {
                throw new Exception("Ожидался оператор! ListOp");
                //return false;
            }

            return res;
        }
        private bool Operator()
        {
            bool res = true;

            switch (allWords[current].word)
            {
                case "Dim":
                    res = Description();
                    //current++;
                    break;
                case "select":
                    res = Switch();
                    //current++;
                    break;
                default :
                    if(Idn.Contains(allWords[current]))
                    {
                        res = Assignment();
                        //current++;
                    }
                    else
                    {
                        throw new Exception("Ожидался оператор какой-то Oper");
                        //res = false;
                    }

                    break;
            }
            return res;
        }
        private bool SpawnOperator()
        {
            bool res = true;

            if(current >= allWords.Count)
            {
                return res;
            }

            switch (allWords[current].word)
            {
                case "case":
                    res = true;
                    break;
                case "end":
                    res = true;
                    break;
                case "select":
                    res = ListOfOperators();
                    break;
                case "Dim":
                    res = ListOfOperators();
                    break;
                default:
                    if (Idn.Contains(allWords[current]))
                    {
                        res = ListOfOperators();
                    }
                    else
                    {
                        throw new Exception("Ожид как опер или что-то SpawnOp");
                        //res = false;
                    }
                    break;
            }

            return res;
        }
        private bool Description()
        {
            bool res = true;

            if(allWords[current].word == "Dim")
            {
                current++;
            }
            else
            {
                throw new Exception("Ожидалось ключевое слово 'Dim'! Descr");
            }
            if(Idn.Contains(allWords[current]))
            {
                res = Identifier();
            }
            if (!res)
            {
                throw new Exception("Ожидался идентификатор! Descr");
            }
            if(allWords[current].word == "as")
            {
                current++;
            }
            else
            {
                throw new Exception("Ожидалось ключевое слово 'as'! Descr");
            }
            if(key.Contains(allWords[current]))
            {
                res = TypeId();
            }
            else
            {
                throw new Exception("Ожидалось объявление типа переменной! Descr");
            }
            if (!res)
            {
                return false;
            }

            return res;
        }

        private bool Switch()
        {
            bool res = true;

            if(allWords[current].word == "select")
            {
                current++;
            }
            else
            {
                throw new Exception("Ожидалось ключевое слово 'select'!sw");
            }
            if (allWords[current].word == "case")
            {
                current++;
            }
            else
            {
                throw new Exception("Ожидалось ключевое слово 'case'! sw");
            }
            if (Idn.Contains(allWords[current]))
            {
                current++;
            }
            else
            {
                throw new Exception("Идент!sw");
            }
            if(allWords[current].word == "\\n")
            {
                current++;
            }
            else
            {
                throw new Exception("Переход! sw");
            }

            if (Cases())
            {
                //current++;
            }
            else
            {
                throw new Exception("Ожидался какой-то кейс! sw");
            }
            if (allWords[current].word == "end")
            {
                current++;
            }
            else
            {
                throw new Exception("Ожидалось ключевое слово 'end'!sw");
            }
            if (allWords[current].word == "select")
            {
                current++;
                res = true;
            }
            else
            {
                throw new Exception("Ожидалось ключевое слово 'select'!sw");
            }


            return res;
        }

        private bool Cases()
        {
            bool res = true;

            if (Case())
            {
                //current++;
            }
            else
            {
                throw new Exception("Ожидалось кайс!Cass");
            }

            if (SpawnCases()) //// plll
            {
                //if(!(allWords[current].word == "case" || allWords[current].word == "end"))
                //{
                //    current++;
                //}
                
            }
            else
            {
                throw new Exception("Ожидалось Spawnкайс!Cass");
            }

            return res;
        }
        private bool Case()
        {
            bool res = true;

            if (ValueCase())
            {
                //current++;
            }
            else
            {
                throw new Exception("Ожидалось кайсValue!Cs");
            }
            if (allWords[current].word == "\\n")
            {
                current++;
            }
            else
            {
                throw new Exception("Переход!Cs");
            }
            if (ListOfOperators())
            {
                //current++;
            }
            else
            {
                throw new Exception("Операторы Cs");
            }

            return res;
        }
        private bool ValueCase() /// aaaa
        {
            bool res = true;

            if(allWords[current].word == "case")
            {
                current++;
            }
            else
            {
                throw new Exception("Case! vs");
            }
            if (!EventValueCase())
            {
                throw new Exception("Event vs");
            }
            if (!(allWords[current].word == "\\n"))
            {
                throw new Exception("Переход vs");
            }

            return res;
        }
        private bool EventValueCase()
        {
            bool res = true;

            switch (allWords[current].word)
            {
                case "else":
                    current++;
                    res = true;
                    break;
                default:
                    if (Lit.Contains(allWords[current]))
                    {
                        res = LitToLit();
                        //current++;
                    }
                    else
                    {
                        throw new Exception("Event или литы Evc");
                    }
                    break;
            }

            return res;
        }
        private bool LitToLit()
        {
            bool res = true;

            if(Lit.Contains(allWords[current]))
            {
                current++;
            }
            else
            {
                throw new Exception("Лит1 ltl");
            }

            switch (allWords[current].word)
            {
                case "to":
                    current++;
                    break;
                default:
                    if(allWords[current].word == "\\n")
                    {
                        res = true;
                        return res;
                    }
                    else
                    {
                        throw new Exception("To или переход ltl");
                    }
            }
            if (!res) 
            {
                throw new Exception("Lits any! ltl");
            }

            if (Lit.Contains(allWords[current]))
            {
                res = true;
                current++;
            }
            else
            {
                throw new Exception("литы ltl");
            }

            return res;
        }
        private bool SpawnCases()
        {
            bool res = true;

            switch (allWords[current].word)
            {
                case "end":
                    res = true;
                    break;
                case "case":
                    res = Cases();
                    break;
                default:
                    res = false;
                    throw new Exception("SpawnCase! SpC");
            }
            return res;
        }
        private bool Assignment()
        {
            bool res = true;

            if (Idn.Contains(allWords[current]))
            {
                current++;
            }
            else
            {
                throw new Exception("Ожидался id! Assig");
            }
            if(allWords[current].word == "=")
            {
                current++;
            }
            else
            {
                throw new Exception("Ожидался '='! Assig");
            }
            Expr();

            //if (!res)
            //{
            //    throw new Exception("What! Assig");
            //    return false;
            //}

            return res;

        }

        private void Expr()
        {
            //current++;
            //return true;
            int currOp = 0, currLitOrId = 0;
            List<Token> expr = new List<Token>();
            while(allWords[current].word != "\\n")
            {
                expr.Add(allWords[current]);
                current++;
                
            }
            Stack<Token> stackOp = new Stack<Token>();
            List<ExprClass> exprList = new List<ExprClass>();
            ExprClass ex = null ;
            foreach (Token t in expr)
            {
                switch (t.type)
                {
                    case "R":
                        if(t.word == ")")
                        {
                            while(stackOp.Peek().word != "(")
                            {
                                ex = new ExprClass(stackOp.Pop().word);
                                exprList.Add(ex);
                                currOp++;
                                if(stackOp.Count == 0)
                                {
                                    throw new Exception("Не хватает открывающей скобки.");
                                }
                            }
                            stackOp.Pop();
                        }
                        else
                        {
                            switch (t.word)
                            {
                                case "(":
                                    stackOp.Push(t);
                                    break;
                                case "*":
                                    if (stackOp.Count > 0)
                                    {
                                        if (stackOp.Peek().word != "(")
                                        {
                                            while (stackOp.Count != 0 && (stackOp.Peek().word == "*" || stackOp.Peek().word == "/"))
                                            {
                                                ex = new ExprClass(stackOp.Pop().word);
                                                exprList.Add(ex);
                                                currOp++;
                                            }
                                            

                                        }
                                    }
                                    stackOp.Push(t);
                                    break;
                                case "/":
                                    if (stackOp.Count > 0)
                                    {
                                        if (stackOp.Peek().word != "(")
                                        {
                                            while (stackOp.Count != 0 && (stackOp.Peek().word == "*" || stackOp.Peek().word == "/"))
                                            {
                                                ex = new ExprClass(stackOp.Pop().word);
                                                exprList.Add(ex);
                                                currOp++;
                                            }
                                            

                                        }
                                    }
                                    stackOp.Push(t);
                                    break;
                                case "+":
                                    if (stackOp.Count > 0)
                                    {
                                        if (!(stackOp.Peek().word != "*" && stackOp.Peek().word != "/" && stackOp.Peek().word == "("))
                                        {
                                            while (stackOp.Count != 0 && stackOp.Peek().word != "(")
                                            {
                                                ex = new ExprClass(stackOp.Pop().word);
                                                exprList.Add(ex);
                                                currOp++;
                                            }
                                            

                                        }
                                    }
                                    stackOp.Push(t);
                                    break;
                                case "-":
                                    if(stackOp.Count > 0) 
                                    {
                                        if (!(stackOp.Peek().word != "*" && stackOp.Peek().word != "/" && stackOp.Peek().word == "("))
                                        {
                                            while (stackOp.Count != 0 && stackOp.Peek().word != "(")
                                            {
                                                ex = new ExprClass(stackOp.Pop().word);
                                                exprList.Add(ex);
                                                currOp++;
                                            }
                                            
                                        }
                                    }
                                    stackOp.Push(t);
                                    break;
                            }

                        }
                        break;
                    case "I":
                        if (key.Contains(t))
                        {
                            throw new Exception($"Использование зарезервированного слова {t.word}");
                        }
                        else
                        {
                            ex = new ExprClass(t.word);
                            exprList.Add(ex);
                            currLitOrId++;
                        }
                        break;
                    default:
                        ex = new ExprClass(t.word);
                        exprList.Add(ex);
                        currLitOrId++;
                        break;
                }

            }
            while (stackOp.Count > 0)
            {
                if(stackOp.Peek().word == "(")
                {
                    throw new Exception("Не хватает закрывающей скобки.");
                }
                ex = new ExprClass(stackOp.Pop().word);
                exprList.Add(ex);
                currOp++;
            }
            if(currLitOrId != currOp + 1)
            {
                throw new Exception("Неправильно построено арифметическое выражение.");
            }
            List<ExprClass> MatrixForm = new List<ExprClass>();
            Stack<ExprClass> stackExpr = new Stack<ExprClass>();

            int i = 0;
            foreach(ExprClass e in exprList)
            {
                if(e.GetValue != "+" && e.GetValue != "-" && e.GetValue != "*" && e.GetValue != "/")
                {
                    stackExpr.Push(e);
                }
                else
                {
                    i++;
                    ex = new ExprClass(stackExpr.Pop(), stackExpr.Pop(), e, i);                   
                    MatrixForm.Add(ex);
                    stackExpr.Push(ex);
                }
            }
            if(stackExpr.Count > 1)
            {
                throw new Exception("Неправильно построено арифметическое выражение.");
            }
            listMatrix.Add(stackExpr.Peek());

        }

        
        private bool Identifier()
        {
            bool res = true;

            if (Idn.Contains(allWords[current]))
            {
                current++;
            }
            else
            {
                throw new Exception("id! Iden");
            }

            if (SpawnIdOne())
            {
                res = true;
            }
            else
            {
                throw new Exception("idSpawn! Iden");
            }

            return res;
        }
        private bool SpawnIdOne()
        {
            bool res = true;

            switch (allWords[current].word)
            {
                case ",":
                    current++;
                    if (Idn.Contains(allWords[current]))
                    {
                        current++;
                    }
                    else
                    {
                        throw new Exception("id! IdenSpawnOne");
                    }
                    if (!SpawnIdTwo())
                    {
                        throw new Exception("idSpTwo! IdenSpawnOne");
                    }
                    
                    break;
                case "as":
                    res = true;
                    break;
                default:
                    res = false;
                    break;
            }

            return res;
        }
        private bool SpawnIdTwo()
        {
            bool res = true;

            switch (allWords[current].word)
            {
                case ",":
                    if (!SpawnIdOne())
                    {
                        throw new Exception("idSpOne! IdenSpawnTwo");
                    }
                    break;
                case "as":
                    res = true;
                    break;
                default:
                    res = false;
                    break;
            }

            return res;
        }
        private bool TypeId()
        {
            bool res = true;

            switch (allWords[current].word)
            {
                case "integer":
                    current++;
                    res = true;
                    break;
                case "float":
                    current++;
                    res = true;
                    break;
                case "double":
                    current++;
                    res = true;
                    break;
                default:
                    throw new Exception("int or ! typeId");
            }

            return res;
        }

        public int Current { get { return current; } }
        public List<ExprClass> ListMatrix { get { return listMatrix; } }
    }
}
