using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lab2_KASR_MAGZ.Models.Data;
using Lab2_KASR_MAGZ.Models;
using ListLibrary;

namespace Lab2_KASR_MAGZ.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: PurchaseController
        public ActionResult Index()
        {
            return View(Singleton.Instance.MedicineList);
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
            return View();
        }

        // POST: PurchaseController/Edit/5
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

        // GET: PurchaseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PurchaseController/Delete/5
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

        [HttpPost]
        public ActionResult ClientInformation(string Name, int NIT, string Direct)
        {
            var newClient = new Models.Customer
            {
                NameCustomer = Name,
                Nit = NIT,
                Direction = Direct
            };
            Singleton.Instance.CustomerListInformation.Add(newClient);

            return RedirectToAction(nameof(Index));
        }
    }
}
