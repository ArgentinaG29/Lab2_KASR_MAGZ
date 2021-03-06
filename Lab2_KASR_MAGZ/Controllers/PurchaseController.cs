using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lab2_KASR_MAGZ.Models.Data;
using Lab2_KASR_MAGZ.Models;
using ListLibrary;
using System.IO;

namespace Lab2_KASR_MAGZ.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: PurchaseController
        public ActionResult Index()
        {
            Singleton.Instance.SearchingData.Clear();
            return View(Singleton.Instance.ProductsList);
        }

        // GET: PurchaseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PurchaseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchaseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PurchaseController/Edit/5
        public ActionResult Edit(int id)
        {
            int pos = 0;
            bool Finding = false;

            while (Finding == false)
            {
                if (Convert.ToInt32(Singleton.Instance.ProductsList.ElementAt(pos).Id) == id)
                {
                    Finding = true;
                }
                else
                {
                    pos++;
                }
            }

            var EditProduct = Singleton.Instance.ProductsList.ElementAt(pos);
            return View(EditProduct);
        }

        // POST: PurchaseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                int pos = 0;
                bool Finding = false;

                while (Finding == false)
                {
                    if (Convert.ToInt32(Singleton.Instance.ProductsList.ElementAt(pos).Id) == id)
                    {
                        Finding = true;
                    }
                    else
                    {
                        pos++;
                    }
                }

                int StockNew = Convert.ToInt32(collection["Stock"]);
                string NameEdit = Singleton.Instance.ProductsList.ElementAt(pos).Name;
                int StockOld = Singleton.Instance.ProductsList.ElementAt(pos).Stock;
                int Stock2Old = Singleton.Instance.MedicineList.ElementAt(id - 1).Stock;

                var EditMedicineS = new Models.Medicine
                {
                    Id = Singleton.Instance.ProductsList.ElementAt(pos).Id,
                    Name = Singleton.Instance.ProductsList.ElementAt(pos).Name,
                    Description = Singleton.Instance.ProductsList.ElementAt(pos).Description,
                    ProductionHouse = Singleton.Instance.ProductsList.ElementAt(pos).ProductionHouse,
                    Price = Singleton.Instance.ProductsList.ElementAt(pos).Price,
                    Stock = Stock2Old + StockOld
                };

                if ((id - 1) == 0)
                {
                    Singleton.Instance.MedicineList.ExtractAtStart();
                    Singleton.Instance.MedicineList.InsertAtStart(EditMedicineS);
                }
                else if (id - 1 == Singleton.Instance.MedicineList.Count())
                {
                    Singleton.Instance.MedicineList.ExtractAtEnd();
                    Singleton.Instance.MedicineList.InsertAtEnd(EditMedicineS);
                }
                else
                {
                    Singleton.Instance.MedicineList.ExtractAtPosition(id - 1);
                    Singleton.Instance.MedicineList.InsertAtPosition(EditMedicineS, id - 1);
                }

                if (StockOld == 0)
                {
                    var NewPD = new Models.Data.ClassMedicine
                    {
                        Position = Singleton.Instance.MedicineList.ElementAt(id).Id,
                        Name = Singleton.Instance.MedicineList.ElementAt(id).Name
                    };
                    Singleton.Instance.IndexList.Insert(NewPD);
                }
                var EditProduct = Singleton.Instance.ProductsList.ElementAt(pos);
                Singleton.Instance.ProductsList.Remove(EditProduct);
                //BUSCAR ELEMENTO
                SearchMedicine(NameEdit, StockNew);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PurchaseController/Delete/5
        public ActionResult Delete(int id)
        {
            int pos = 0;
            bool Finding = false;
            while (Finding == false)
            {
                if (Convert.ToInt32(Singleton.Instance.ProductsList.ElementAt(pos).Id) == id)
                {
                    Finding = true;
                }
                else
                {
                    pos++;
                }
            }
            var DeleteProduct = Singleton.Instance.ProductsList.ElementAt(pos);
            return View(DeleteProduct);
        }

        // POST: PurchaseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            int pos = 0;
            bool Finding = false;
            try
            {
                while (Finding == false)
                {
                    if (Convert.ToInt32(Singleton.Instance.ProductsList.ElementAt(pos).Id) == id)
                    {
                        Finding = true;
                    }
                    else
                    {
                        pos++;
                    }
                }
                var DeleteProduct = Singleton.Instance.ProductsList.ElementAt(pos);

                int NewCant = Singleton.Instance.ProductsList.ElementAt(pos).Stock;
                //AGREAGR A LA LISTA ORIGINAL
                var MedicineDelete = new Models.Medicine
                {
                    Id= Singleton.Instance.MedicineList.ElementAt(id - 1).Id,
                    Name = Singleton.Instance.MedicineList.ElementAt(id - 1).Name,
                    Description = Singleton.Instance.MedicineList.ElementAt(id - 1).Description,
                    ProductionHouse= Singleton.Instance.MedicineList.ElementAt(id - 1).ProductionHouse,
                    Price= Singleton.Instance.MedicineList.ElementAt(id - 1).Price,
                    Stock= Singleton.Instance.MedicineList.ElementAt(id - 1).Stock + NewCant
                };

                //AGREGAR AL ARBOL SI ES CERO
                if (Convert.ToInt32(Singleton.Instance.MedicineList.ElementAt(id - 1).Stock) != 0)
                {
                    var NewPD = new Models.Data.ClassMedicine
                    {
                        Position = Singleton.Instance.MedicineList.ElementAt(id).Id,
                        Name = Singleton.Instance.MedicineList.ElementAt(id).Name
                    };
                    Singleton.Instance.IndexList.Insert(NewPD);
                }

                if ((id - 1) == 0)
                {
                    Singleton.Instance.MedicineList.ExtractAtStart();
                    Singleton.Instance.MedicineList.InsertAtStart(MedicineDelete);
                }
                else if (id - 1 == Singleton.Instance.MedicineList.Count())
                {
                    Singleton.Instance.MedicineList.ExtractAtEnd();
                    Singleton.Instance.MedicineList.InsertAtEnd(MedicineDelete);
                }
                else
                {
                    Singleton.Instance.MedicineList.ExtractAtPosition(id - 1);
                    Singleton.Instance.MedicineList.InsertAtPosition(MedicineDelete, id - 1);
                }

                //RETIRAR DE LA LISTA DE PRODUCTOS
                Singleton.Instance.ProductsList.Remove(DeleteProduct);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult ClientInformation(string Name, int NIT, string Direct)
        { 
            if(Singleton.Instance.CustomerListInformation.Count() == 0)
            {
                var newClient = new Models.Customer
                {
                    NameCustomer = Name,
                    Nit = NIT,
                    Direction = Direct
                };
                Singleton.Instance.CustomerListInformation.Add(newClient);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult SearchMed(string NameMS)
        {
            var NewSearch = new Models.Data.ClassMedicine
            {
                Name = NameMS
            };

            TreeNode<ClassMedicine> new_node = new TreeNode<ClassMedicine>();
            new_node = Singleton.Instance.IndexList.Search(NewSearch, Singleton.Instance.IndexList.ReturnRoot());
            int Line = new_node.value.Position;
            if (Line != 0)
            {
                var OldProduct = new Models.Medicine
                {
                    Id = Line,
                    Name = Singleton.Instance.MedicineList.ElementAt(Line - 1).Name,
                    Description = Singleton.Instance.MedicineList.ElementAt(Line - 1).Description,
                    ProductionHouse = Singleton.Instance.MedicineList.ElementAt(Line - 1).ProductionHouse,
                    Price = Singleton.Instance.MedicineList.ElementAt(Line - 1).Price,
                    Stock = Singleton.Instance.MedicineList.ElementAt(Line - 1).Stock
                };
                Singleton.Instance.SearchingData.Add(OldProduct);
            }

            return View(Singleton.Instance.SearchingData);
        }


        [HttpPost]
        public ActionResult SearchMedicine(string NameMS, int quantity)
        {
            try
            {
                var NewSearch = new Models.Data.ClassMedicine
                {
                    Name = NameMS
                };

                TreeNode<ClassMedicine> new_node = new TreeNode<ClassMedicine>();
                new_node = Singleton.Instance.IndexList.Search(NewSearch, Singleton.Instance.IndexList.ReturnRoot());
                int Line = 0;
                if (new_node != null)
                {
                    Line = new_node.value.Position;
                }
                
                if (Line != 0)
                {
                    int StockAvailable = Singleton.Instance.MedicineList.ElementAt(Line - 1).Stock;
                    int Unsold = StockAvailable - quantity;

                    if (quantity <= StockAvailable)
                    {
                        var NewProduct = new Models.Medicine
                        {
                            Id = Line,
                            Name = Singleton.Instance.MedicineList.ElementAt(Line - 1).Name,
                            Description = Singleton.Instance.MedicineList.ElementAt(Line - 1).Description,
                            ProductionHouse = Singleton.Instance.MedicineList.ElementAt(Line - 1).ProductionHouse,
                            Price = Singleton.Instance.MedicineList.ElementAt(Line - 1).Price,
                            Stock = quantity
                        };

                        var NewUnsold = new Models.Medicine
                        {
                            Id = Line,
                            Name = Singleton.Instance.MedicineList.ElementAt(Line - 1).Name,
                            Description = Singleton.Instance.MedicineList.ElementAt(Line - 1).Description,
                            ProductionHouse = Singleton.Instance.MedicineList.ElementAt(Line - 1).ProductionHouse,
                            Price = Singleton.Instance.MedicineList.ElementAt(Line - 1).Price,
                            Stock = Unsold
                        };

                        int contando = Singleton.Instance.ProductsList.Count();
                        if (contando != 0)
                        {
                            Singleton.Instance.ProductsList.RemoveAt(contando - 1);
                        }

                        //ELIMINAR Y ACTUALIZAR NUEVO VALOR DE STOCK
                        if ((Line - 1) == 0)
                        {
                            Singleton.Instance.MedicineList.ExtractAtStart();
                            Singleton.Instance.MedicineList.InsertAtStart(NewUnsold);
                        }
                        else if (Line - 1 == Singleton.Instance.MedicineList.Count())
                        {
                            Singleton.Instance.MedicineList.ExtractAtEnd();
                            Singleton.Instance.MedicineList.InsertAtEnd(NewUnsold);
                        }
                        else
                        {
                            Singleton.Instance.MedicineList.ExtractAtPosition(Line - 1);
                            Singleton.Instance.MedicineList.InsertAtPosition(NewUnsold, Line - 1);
                        }

                        //AGREGAR A LA NUEVA LISTA
                        Singleton.Instance.ProductsList.Add(NewProduct);

                        double TotalNumber = Calculations.NTotal(Singleton.Instance.ProductsList);
                        var NewTotal = new Models.Medicine
                        {
                            Name = "TOTAL",
                            Description = "-",
                            ProductionHouse = "-",
                            Id = 000,
                            Stock = Calculations.ProductTotal(Singleton.Instance.ProductsList),
                            Price = TotalNumber
                        };

                        Singleton.Instance.ProductsList.Add(NewTotal);

                        if (Unsold == 0)
                        {
                            //ELIMINAR
                            Singleton.Instance.IndexList.Delete(NewSearch, Singleton.Instance.IndexList.ReturnRoot());
                        }
                        

                        
                    }
                    else
                    {
                        var OldProduct = new Models.Medicine
                        {
                            Id = Line,
                            Name = Singleton.Instance.MedicineList.ElementAt(Line - 1).Name,
                            Description = Singleton.Instance.MedicineList.ElementAt(Line - 1).Description,
                            ProductionHouse = Singleton.Instance.MedicineList.ElementAt(Line - 1).ProductionHouse,
                            Price = Singleton.Instance.MedicineList.ElementAt(Line - 1).Price,
                            Stock = Singleton.Instance.MedicineList.ElementAt(Line - 1).Stock
                        };
                        Singleton.Instance.SearchingData.Add(OldProduct);

                        return View(Singleton.Instance.SearchingData);
                    }
                }
                else
                {
                    return View(Singleton.Instance.SearchingData);
                }

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult DownloadBill()
        {
            string text = "";
            text += "Customer name: " + Singleton.Instance.CustomerListInformation.ElementAt(0).NameCustomer + "\n" + "NIT: " + Singleton.Instance.CustomerListInformation.ElementAt(0).Nit + "\n"+ "Address: " + Singleton.Instance.CustomerListInformation.ElementAt(0).Direction + "\n";


            for (int i = 0; i < Singleton.Instance.ProductsList.Count; i++)
            {
                text += Singleton.Instance.ProductsList.ElementAt(i).Name + ", ";
                text += Singleton.Instance.ProductsList.ElementAt(i).Description + ", ";
                text += Singleton.Instance.ProductsList.ElementAt(i).ProductionHouse + ", ";
                text += Singleton.Instance.ProductsList.ElementAt(i).Price + ", ";
                text += Singleton.Instance.ProductsList.ElementAt(i).Stock + ", " + "\n";
            }
            StreamWriter writer = new StreamWriter("Bill.txt");
            writer.Write(text);
            writer.Close();
            return RedirectToAction(nameof(Index));
        }
    }
}
