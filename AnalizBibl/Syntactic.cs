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
                    if(current > allWords.Count - 1)
                    {
                        break;
                    }
                    if (allWords[current].word == "end" || allWords[current].word == "case")
                    {
                        throw new Exception($"Использование end или case вне оператора switch! Необходимо объявление переменных, лексема {current}, Program.");
                    }
                    break;
                //case "select":
                //    break;
                //case "id":
                //    break;
                default:
                    throw new Exception($"Ожидался оператор Dim! Необходимо объявление переменных, лексема {current}, Program.");
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
                throw new Exception($"Ожидался переход на новую строку, лексема {current}, ListOfOperators.");
                //return false;
            }

            if (SpawnOperator())
            {
                //current++;
            }
            else
            {
                throw new Exception($"Ожидался оператор! Лексема {current}, ListOfOperators");
                //return false;
            }

            return res;
        }
        private bool Operator()
        {
            bool res = true;
            if (current > allWords.Count - 1)
            {
                throw new Exception($"Ожидались новые операторы, лексема {current}, Operator");
            }

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
                        throw new Exception($"Ожидался оператор, лексема {current}, Operator()");
                        //res = false;
                    }

                    break;
            }
            return res;
        }
        private bool SpawnOperator()
        {
            bool res = true;

            if(current > allWords.Count - 1)
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
                    if (current > allWords.Count - 1)
                    {
                        return res;
                    }
                    if (allWords[current].word == "end" || allWords[current].word == "case")
                    {
                        //throw new Exception($"Использование end или case вне оператора switch! Необходимо объявление переменных, лексема {current}, SpawnOperator.");
                    }
                    break;
                case "Dim":
                    res = ListOfOperators();
                    if (current > allWords.Count - 1)
                    {
                        return res;
                    }
                    if (allWords[current].word == "end" || allWords[current].word == "case")
                    {
                        //throw new Exception($"Использование end или case вне оператора switch! Необходимо объявление переменных, лексема {current}, SpawnOperator.");
                    }
                    break;
                default:
                    if (Idn.Contains(allWords[current]))
                    {
                        res = ListOfOperators();
                        if (current > allWords.Count - 1)
                        {
                            return res;
                        }
                        if (allWords[current].word == "end" || allWords[current].word == "case")
                        {
                            //throw new Exception($"Использование end или case вне оператора switch! Необходимо объявление переменных, лексема {current}, SpawnOperator.");
                        }
                    }
                    else
                    {
                        throw new Exception($"Ожидался оператор или его продолжение, лексема {current}, SpawnOperator");
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
                throw new Exception($"Ожидалось ключевое слово 'Dim'!, лексема {current}, Description");
            }
            if (current >= allWords.Count - 1)
            {
                throw new Exception($"Ожидалось перечисление переменных! Лексема {current}, Description");
            }
            if (Idn.Contains(allWords[current]))
            {
                res = Identifier();
            }
            else
            {
                throw new Exception($"Ожидался идентификатор! Лексема {current}, Description");
            }
            if (!res)
            {
                throw new Exception($"Ожидался идентификатор! Лексема {current}, Description");
            }
            if (current >= allWords.Count)
            {
                throw new Exception($"Ожидалось ключевое слово 'as'! Лексема {current}, Description");
            }
            if (allWords[current].word == "as")
            {
                current++;
            }
            else
            {
                throw new Exception($"Ожидалось ключевое слово 'as'! Лексема {current}, Description");
            }
            if (current >= allWords.Count)
            {
                throw new Exception($"Ожидалось объявление типа переменной! Лексема {current}, Description");
            }
            if (key.Contains(allWords[current]))
            {
                res = TypeId();
            }
            else
            {
                throw new Exception($"Ожидалось объявление типа переменной! Лексема {current}, Description");
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
                throw new Exception($"Ожидалось ключевое слово 'select'! Лексема {current}, Switch");
            }
            if (current >= allWords.Count)
            {
                throw new Exception($"Ожидалось ключевое слово 'case'! Лексема {current}, Switch");
            }
            if (allWords[current].word == "case")
            {
                current++;
            }
            else
            {
                throw new Exception($"Ожидалось ключевое слово 'case'! Лексема {current}, Switch");
            }
            if (current >= allWords.Count)
            {
                throw new Exception($"Ожидался идентификатор, лексема {current}, Switch");
            }
            if (Idn.Contains(allWords[current]))
            {
                current++;
            }
            else
            {
                throw new Exception($"Ожидался идентификатор, лексема {current}, Switch");
            }
            if (current >= allWords.Count)
            {
                throw new Exception($"Ожидался случай case, лексема {current}, Switch");
            }
            if (allWords[current].word == "\\n")
            {
                current++;
            }
            else
            {
                throw new Exception($"Ожидался переход на новую стороку, лексема {current}, Switch");
            }

            if (Cases())
            {
                //current++;
            }
            else
            {
                throw new Exception($"Ожидался case, лексема {current}, Switch");
            }
            if (allWords[current].word == "end")
            {
                current++;
            }
            else
            {
                throw new Exception($"Ожидалось ключевое слово 'end'! Лексема {current}, Switch");
            }
            if (current >= allWords.Count)
            {
                throw new Exception($"Ожидалось ключевое слово 'select', лексема {current}, Switch");
            }
            if (allWords[current].word == "select")
            {
                current++;
                res = true;
            }
            else
            {
                throw new Exception($"Ожидалось ключевое слово 'select', лексема {current}, Switch");
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
                throw new Exception($"Ожидалось case! Лексема {current}, Cases");
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
                throw new Exception($"Ожидалось case, лексема {current}, Cases");
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
                throw new Exception($"Ожидалось значение case! Лексема {current}, Case");
            }
            if (allWords[current].word == "\\n")
            {
                current++;
            }
            else
            {
                throw new Exception($"Ожидался переход, лексема {current}, Case");
            }
            if (current >= allWords.Count)
            {
                throw new Exception($"Ожидался оператор, лексема {current}, Case");
            }
            if (ListOfOperators())
            {
                //current++;
            }
            else
            {
                throw new Exception($"Ожидался оператор, лексема {current}, Case");
            }

            return res;
        }
        private bool ValueCase() 
        {
            bool res = true;
            if (current > allWords.Count - 1)
            {
                throw new Exception($"Ожидалось ключевое слово 'case', лексема {current}, ValueCase");
            }
            if (allWords[current].word == "case")
            {
                current++;
            }
            else
            {
                throw new Exception($"Ожидалось ключевое слово 'case', лексема {current}, ValueCase");
            }
            
            if (!EventValueCase())
            {
                throw new Exception($"Возможные значения case, лексема {current}, ValueCase");
            }
            if(current > allWords.Count - 1)
            {
                throw new Exception($"Ожидался оператор, лексема {current}, ValueCase");
            }
            if (!(allWords[current].word == "\\n"))
            {
                throw new Exception($"Ожидался переход, лексема {current}, ValueCase");
            }

            return res;
        }
        private bool EventValueCase()
        {
            bool res = true;
            if (current > allWords.Count - 1)
            {
                throw new Exception($"Ожидались числа или ключевое слово 'else', лексема {current}, EventValueCase");
            }
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
                        throw new Exception($"Ожидались числа или ключевое слово 'else', лексема {current}, EventValueCase");
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
                throw new Exception($"Ожидалось число, лексема {current}, LitToLit");
            }
            if(current >= allWords.Count)
            {
                throw new Exception($"Ожидалось число или оператор, лексема {current}, LitToLit");
            }
            switch (allWords[current].word)
            {
                case "to":
                    current++;
                    if (current >= allWords.Count)
                    {
                        throw new Exception($"Ожидалось число, лексема {current}, LitToLit");
                    }
                    break;
                default:
                    if(allWords[current].word == "\\n")
                    {
                        res = true;
                        return res;
                    }
                    else
                    {
                        throw new Exception($"Ожидались переход или ключевое слово 'to', лексема {current}, LitToLit");
                    }
            }
            if (!res) 
            {
                throw new Exception($"Ожидались литералы, лексема {current}, LitToLit");
            }

            if (Lit.Contains(allWords[current]))
            {
                res = true;
                current++;
            }
            else
            {
                throw new Exception($"Ожидались литералы, лексема {current}, LitToLit");
            }

            return res;
        }
        private bool SpawnCases()
        {
            bool res = true;
            if(current > allWords.Count - 1)
            {
                throw new Exception($"Ожидались case или ключевое слово 'end', лексема {current}, SpawnCases");
            }
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
                    throw new Exception($"Ожидались case или ключевое слово 'end', лексема {current}, SpawnCases");
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
                throw new Exception($"Ожидался идентификатор, лексема {current}, Assignment");
            }
            if (current >= allWords.Count - 1)
            {
                throw new Exception($"Ожидался '=' или вы неправильно ввели ключевое слово, лексема {current}, Assignment");
            }
            if (allWords[current].word == "=")
            {
                current++;
            }
            else
            {
                throw new Exception($"Ожидался '=' или вы неправильно ввели ключевое слово, лексема {current}, Assignment");
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

            while(current < allWords.Count && allWords[current].word != "\\n")
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
                            throw new Exception($"Использование зарезервированного слова {t.word} в выражении (заканчивается оно на {current} лексеме)");
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
            if (current >= allWords.Count - 1)
            {
                throw new Exception($"Ожидалось перечисление переменных! Лексема {current}, Identifier");
            }
            if (Idn.Contains(allWords[current]))
            {
                current++;
            }
            else
            {
                throw new Exception($"Ожидался идентификатор, лексема {current}, Identifier");
            }

            if (SpawnIdOne())
            {
                res = true;
            }
            else
            {
                throw new Exception($"Ожидались идентификаторы или объявление типа переменной, лексема {current}, Identifier");
            }

            return res;
        }
        private bool SpawnIdOne()
        {
            bool res = true;
            if (current >= allWords.Count)
            {
                throw new Exception($"Ожидалось перечисление переменных! Лексема {current}, SpawnIdOne");
            }
            switch (allWords[current].word)
            {
                case ",":
                    current++;
                    if (current >= allWords.Count)
                    {
                        throw new Exception($"Ожидалось перечисление переменных! Лексема {current}, SpawnIdOne");
                    }
                    if (Idn.Contains(allWords[current]))
                    {
                        current++;
                    }
                    else
                    {
                        throw new Exception($"Ожидался идентификатор, лексема {current}, SpawnIdOne");
                    }
                    if (!SpawnIdTwo())
                    {
                        throw new Exception($"Ожидалось перечисление идентификаторов, лексема {current}, SpawnIdOne");
                    }
                    
                    break;
                case "as":
                    res = true;
                    break;
                default:
                    throw new Exception($"Ожидались ',' или 'as', лексема {current}, SpawnIdOne");
            }

            return res;
        }
        private bool SpawnIdTwo()
        {
            bool res = true;
            if (current >= allWords.Count)
            {
                throw new Exception($"Ожидалось перечисление переменных! Лексема {current}, SpawnIdTwo");
            }
            switch (allWords[current].word)
            {
                case ",":
                    if (!SpawnIdOne())
                    {
                        throw new Exception($"Ожидалось перечисление переменной, лексема {current}, SpawnIdTwo");
                    }
                    break;
                case "as":
                    res = true;
                    break;
                default:
                    res = false;
                    throw new Exception($"Ожидались ',' или 'as', лексема {current}, SpawnIdTwo");
            }

            return res;
        }
        private bool TypeId()
        {
            bool res = true;
            if (current >= allWords.Count)
            {
                throw new Exception($"Ожидалось объявление типа! Лексема {current},TypeId");
            }
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
                    throw new Exception($"Ожидалось объявление типа, лексема {current}, TypeId");
            }

            return res;
        }

        public int Current { get { return current; } }
        public List<ExprClass> ListMatrix { get { return listMatrix; } }
    }
}
