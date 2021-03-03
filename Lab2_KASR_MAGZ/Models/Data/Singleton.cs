using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListLibrary;

namespace Lab2_KASR_MAGZ.Models.Data
{
    public class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
        public GenericList<Medicine> MedicineList;

        private Singleton()
        {
            MedicineList = new DoubleLinkedList<Medicine>();
        }

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
