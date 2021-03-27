using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2_KASR_MAGZ.Models.Data;
using Lab2_KASR_MAGZ.Models;

namespace Lab2_KASR_MAGZ.Models.Data
{
    public class ClassMedicine: IComparable
    {
        public string Name;
        public int Position;

        public ClassMedicine()
        {
            Name = "";
            Position = 0;
        }

        public int CompareTo(object obj)
        {
            var comparer = ((ClassMedicine)obj).Name;
            bool flag = false;
            int i = 0;
            int result = 0;

            if(comparer.ToUpper() != Name.ToUpper())
            {
                while(flag == false && i < comparer.Length && i < Name.Length)
                {
                    if (GetNumber(comparer.Substring(i, 1).ToUpper()) < GetNumber(Name.Substring(i, 1).ToUpper()))
                    {
                        result = 1;
                        flag = true;
                    }
                    else
                    {
                        if (GetNumber(comparer.Substring(i, 1).ToUpper()) > GetNumber(Name.Substring(i, 1).ToUpper()))
                        {
                            result = -1;
                            flag = true;
                        }
                        else
                        {
                            i++;
                        }
                    }
                }

                if (flag == false)
                {
                    if (i == Name.Length)
                    {
                        result = -1;
                    }
                    else
                    {
                        if (i == comparer.Length)
                        {
                            result = 1;
                        }
                    }
                }
            }
            return result;
        }

        public void InformationTree(object obj)
        {
            var infot = ((ClassMedicine)obj).Name;
            string Aux = Convert.ToString(infot);

            var InformationFileD = new Models.FileDownload
            {
                NameMedicineFile = Aux
            };

            Singleton.Instance.InformationFile.Add(InformationFileD);
            
        }
        public override bool Equals(object obj)
        {
            InformationTree(obj);
            return true;
        }

        public int GetNumber(string letter)
        {
            int number = 0;
            switch (letter)
            {
                case ("A"):
                    number = 1;
                    break;
                case ("B"):
                    number = 2;
                    break;
                case ("C"):
                    number = 3;
                    break;
                case ("D"):
                    number = 4;
                    break;
                case ("E"):
                    number = 5;
                    break;
                case ("F"):
                    number = 6;
                    break;
                case ("G"):
                    number = 7;
                    break;
                case ("H"):
                    number = 8;
                    break;
                case ("I"):
                    number = 9;
                    break;
                case ("J"):
                    number = 10;
                    break;
                case ("K"):
                    number = 11;
                    break;
                case ("L"):
                    number = 12;
                    break;
                case ("M"):
                    number = 13;
                    break;
                case ("N"):
                    number = 14;
                    break;
                case ("O"):
                    number = 15;
                    break;
                case ("P"):
                    number = 16;
                    break;
                case ("Q"):
                    number = 17;
                    break;
                case ("R"):
                    number = 18;
                    break;
                case ("S"):
                    number = 19;
                    break;
                case ("T"):
                    number = 20;
                    break;
                case ("U"):
                    number = 21;
                    break;
                case ("V"):
                    number = 22;
                    break;
                case ("W"):
                    number = 23;
                    break;
                case ("X"):
                    number = 24;
                    break;
                case ("Y"):
                    number = 25;
                    break;
                case ("Z"):
                    number = 26;
                    break;
                case (" "):
                    number = 27;
                    break;                
                default:
                    break;
            }

            return number;
        }
    }
}
