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

                    var FileMedicine = new Models.Medicine
                    {
                        Id = IdNumber,
                        Name = FileName,
                        Description = FileDescription,
                        ProductionHouse = FileHome,
                        Price = FilePrice,
                        Stock = FileStock
                    };

                    if(IdNumber == 1)
                    {
                        Singleton.Instance.MedicineList.InsertAtStart(FileMedicine);
                    }
                    else
                    {
                        Singleton.Instance.MedicineList.InsertAtEnd(FileMedicine);
                    }
                    IdNumber++;
                }

                return RedirectToAction(nameof(Index));
            }

            return View();
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
    }
}
