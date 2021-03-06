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
        public List<Customer> CustomerListInformation;
        public Tree<ClassMedicine> IndexList;
        public List<FileDownload> InformationFile;
        public List<Medicine> ProductsList;
        public List<Medicine> SearchingData;

        private Singleton()
        {
            MedicineList = new DoubleLinkedList<Medicine>();
            CustomerListInformation = new List<Customer>();
            IndexList = new BinaryTree<ClassMedicine>();
            InformationFile = new List<FileDownload>();
            ProductsList = new List<Medicine>();
            SearchingData = new List<Medicine>();
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
