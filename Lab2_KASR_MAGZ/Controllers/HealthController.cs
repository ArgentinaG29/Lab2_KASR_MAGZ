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
using System.Data;
using Microsoft.AspNetCore.Hosting;

namespace Lab2_KASR_MAGZ.Controllers
{
    public class HealthController : Controller
    {
        private IHostingEnvironment Environment;

        public HealthController(IHostingEnvironment _environment)
        {
            Environment = _environment;
            Singleton.Instance.CustomerListInformation.Clear();
            Singleton.Instance.ProductsList.Clear();
        }

        // GET: HealthController
        public ActionResult Index()
        {
            return View(Singleton.Instance.MedicineList);
        }

        [HttpPost]
        public IActionResult Index(IFormFile postedFile)
        {
            int IdNumber = 1;
            if(postedFile != null)
            {
                bool Same = false;
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                string csvData = System.IO.File.ReadAllText(filePath);

                StreamReader streamReader = new StreamReader(filePath);
                string CurrentLine;

                while (!streamReader.EndOfStream)
                {
                    CurrentLine = streamReader.ReadLine();
                    Same = false;
                    string[] FileInformationList = CurrentLine.Split(',');
                    int pos = 1;
                    string FileName = "";
                    string FileDescription = "";
                    string FileHome = "";
                    string FileNamePrice = "";

                    bool Repeat = true;
                    bool Primero = true;
                    string Concatenando="";
                    if (FileInformationList[pos].Substring(0, 1) == "\"")
                    {
                        while (Repeat == true)
                        {
                            if(Primero == true)
                            {
                                Concatenando += FileInformationList[pos];
                                Primero = false;
                            }
                            else
                            {
                                Concatenando += "," + FileInformationList[pos];
                            }
                            
                            string FileLength = FileInformationList[pos];
                            if (FileInformationList[pos].Substring(FileLength.Length - 1, 1) == "\"")
                            {
                                Repeat = false;
                                Concatenando = Concatenando.Remove(0, 2);
                                Concatenando = Concatenando.Remove(Concatenando.Length - 2, 2);
                            }
                            pos++;
                        }
                    }
                    else
                    {
                        Concatenando = FileInformationList[pos];
                        pos++;
                    }
                    FileName = Concatenando;

                    Repeat = true;
                    Primero = true;
                    Concatenando = "";
                    if (FileInformationList[pos].Substring(0, 1) == "\"")
                    {
                        while (Repeat == true)
                        {
                            if (Primero == true)
                            {
                                Concatenando += FileInformationList[pos];
                                Primero = false;
                            }
                            else
                            {
                                Concatenando += ", " + FileInformationList[pos];
                            }

                            string FileLength = FileInformationList[pos];
                            if (FileInformationList[pos].Substring(FileLength.Length - 1, 1) == "\"")
                            {
                                Repeat = false;
                                Concatenando = Concatenando.Remove(0, 2);
                                Concatenando = Concatenando.Remove(Concatenando.Length - 2, 2);
                            }
                            pos++;
                        }
                    }
                    else
                    {
                        Concatenando = FileInformationList[pos];
                        pos++;
                    }

                    FileDescription = Concatenando;

                    Repeat = true;
                    Primero = true;
                    Concatenando = "";
                    if (FileInformationList[pos].Substring(0, 1) == "\"")
                    {
                        while (Repeat == true)
                        {
                            if (Primero == true)
                            {
                                Concatenando += FileInformationList[pos];
                                Primero = false;
                            }
                            else
                            {
                                Concatenando += ", " + FileInformationList[pos];
                            }
                            string FileLength = FileInformationList[pos];
                            if (FileInformationList[pos].Substring(FileLength.Length - 1, 1) == "\"")
                            {
                                Repeat = false;
                                Concatenando = Concatenando.Remove(0, 2);
                                Concatenando = Concatenando.Remove(Concatenando.Length - 2, 2);
                            }
                            pos++;
                        }
                    }
                    else
                    {
                        Concatenando = FileInformationList[pos];
                        pos++;
                    }

                    FileHome = Concatenando;

                    string FileLength2 = FileInformationList[pos];
                    FileNamePrice = FileInformationList[pos].Substring(1, FileLength2.Length - 1);
                    double FilePrice = Convert.ToDouble(FileNamePrice);
                    pos++;
                    string RemoveSomeL = FileInformationList[pos];
                    string removeSome = "";
                    if (FileInformationList[pos].Substring(RemoveSomeL.Length - 1, 1) == "\"")
                    {
                        removeSome = FileInformationList[pos].Remove(RemoveSomeL.Length - 1, 1);
                    }
                    else
                    {
                        removeSome = FileInformationList[pos];
                    }
                    
                    int FileStock = Convert.ToInt32(removeSome);

                    for(int i=0; i< Singleton.Instance.MedicineList.Count(); i++)
                    {
                        string CompNameA = FileName.ToUpper();
                        string CompNameB = Convert.ToString(Singleton.Instance.MedicineList.ElementAt(i).Name).ToUpper();
                        if (FileName.ToUpper() == Convert.ToString(Singleton.Instance.MedicineList.ElementAt(i).Name).ToUpper())
                        {
                            Same = true;
                        }
                    }

                    if(Same == false)
                    {
                        var FileMedicine = new Models.Medicine
                        {
                            Id = IdNumber,
                            Name = FileName,
                            Description = FileDescription,
                            ProductionHouse = FileHome,
                            Price = FilePrice,
                            Stock = FileStock
                        };

                        var FileIndex = new Models.Data.ClassMedicine
                        {
                            Position = IdNumber,
                            Name = FileName
                        };

                        if (IdNumber == 1)
                        {
                            Singleton.Instance.MedicineList.InsertAtStart(FileMedicine);
                        }
                        else
                        {
                            Singleton.Instance.MedicineList.InsertAtEnd(FileMedicine);
                        }

                        Singleton.Instance.IndexList.Insert(FileIndex);
                        IdNumber++;

                    }

                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: HealthController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HealthController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HealthController/Create
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

        // GET: HealthController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HealthController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: HealthController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HealthController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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



        public ActionResult NewRessuply()
        {
            if(Singleton.Instance.MedicineList.Count() != 0)
            {
                for (int i = 0; i < Singleton.Instance.MedicineList.Count(); i++)
                {
                    if (Singleton.Instance.MedicineList.ElementAt(i).Stock == 0)
                    {
                        int IdNumer = Singleton.Instance.MedicineList.ElementAt(i).Id;

                        int NewStockMedicine = Calculations.resupply();
                        var NewMedicineStock = new Models.Medicine
                        {
                            Id = Singleton.Instance.MedicineList.ElementAt(i).Id,
                            Name = Singleton.Instance.MedicineList.ElementAt(i).Name,
                            Description = Singleton.Instance.MedicineList.ElementAt(i).Description,
                            ProductionHouse = Singleton.Instance.MedicineList.ElementAt(i).ProductionHouse,
                            Price = Singleton.Instance.MedicineList.ElementAt(i).Price,
                            Stock = NewStockMedicine
                        };

                        var NewIndexStock = new Models.Data.ClassMedicine
                        {
                            Position = IdNumer,
                            Name = Singleton.Instance.MedicineList.ElementAt(i).Name
                        };

                        if (i == 0)
                        {
                            Singleton.Instance.MedicineList.ExtractAtStart();
                            Singleton.Instance.MedicineList.InsertAtStart(NewMedicineStock);
                        }
                        else if (i == Singleton.Instance.MedicineList.Count() - 1)
                        {
                            Singleton.Instance.MedicineList.ExtractAtEnd();
                            Singleton.Instance.MedicineList.InsertAtEnd(NewMedicineStock);
                        }
                        else
                        {
                            Singleton.Instance.MedicineList.ExtractAtPosition(i);
                            Singleton.Instance.MedicineList.InsertAtPosition(NewMedicineStock, i);
                        }

                        Singleton.Instance.IndexList.Insert(NewIndexStock);

                    }
                }
            }
            
            return RedirectToAction(nameof(Index));
        }
        public ActionResult ListPre()
        {
            Singleton.Instance.InformationFile.Clear();
            Singleton.Instance.IndexList.PreOrder(Singleton.Instance.IndexList.ReturnRoot());
            return View(Singleton.Instance.InformationFile);
        }

        public ActionResult ListIn()
        {
            Singleton.Instance.InformationFile.Clear();
            Singleton.Instance.IndexList.InOrder(Singleton.Instance.IndexList.ReturnRoot());
            return View(Singleton.Instance.InformationFile);
        }

        public ActionResult ListPost()
        {
            Singleton.Instance.InformationFile.Clear();
            Singleton.Instance.IndexList.PostOrder(Singleton.Instance.IndexList.ReturnRoot());
            return View(Singleton.Instance.InformationFile);
        }

        public ActionResult DownloadPre()
        {
            string text = "";
            Singleton.Instance.InformationFile.Clear();

            Singleton.Instance.IndexList.PreOrder(Singleton.Instance.IndexList.ReturnRoot());

            for (int i = 0; i < Singleton.Instance.InformationFile.Count; i++)
            {
                string NameRoute = Singleton.Instance.InformationFile.ElementAt(i).NameMedicineFile;
                text += NameRoute + ", ";
            }
            StreamWriter writer = new StreamWriter("PreOrder.txt");
            writer.Write(text);
            writer.Close();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult DownloadIn()
        {
            string text = "";
            Singleton.Instance.InformationFile.Clear();

            Singleton.Instance.IndexList.InOrder(Singleton.Instance.IndexList.ReturnRoot());

            for (int i =0; i<Singleton.Instance.InformationFile.Count; i++)
            {
                string NameRoute = Singleton.Instance.InformationFile.ElementAt(i).NameMedicineFile;
                text += NameRoute + ", ";
            }
            StreamWriter writer = new StreamWriter("InOrder.txt");
            writer.Write(text);
            writer.Close();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult DownloadPost()
        {
            string text = "";
            Singleton.Instance.InformationFile.Clear();

            Singleton.Instance.IndexList.PostOrder(Singleton.Instance.IndexList.ReturnRoot());

            for (int i = 0; i < Singleton.Instance.InformationFile.Count; i++)
            {
                string NameRoute = Singleton.Instance.InformationFile.ElementAt(i).NameMedicineFile;
                text += NameRoute + ", ";
            }
            StreamWriter writer = new StreamWriter("PostOrder.txt");
            writer.Write(text);
            writer.Close();
            return RedirectToAction(nameof(Index));
        }
    }
}
