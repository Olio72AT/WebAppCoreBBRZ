using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreBBRZ.Models;

namespace WebAppCoreBBRZ.Controllers
{
    public class PersistenceController : Controller
    {
        public static string filename = "admindata.json";

        // GET: PersistenceController
        public ActionResult Index()
        {
            JavaScriptSerializer javascriptSerializer = new JavaScriptSerializer();
            var ourJSONData = javascriptSerializer.Serialize(WebAppCoreBBRZ.Controllers.TodoController.todoListe);
            ViewData["JSON"] = ourJSONData;

            return View();
        }

        public ActionResult SaveJSON()
        {
            JavaScriptSerializer javascriptSerializer = new JavaScriptSerializer();
            var ourJSONData = javascriptSerializer.Serialize(WebAppCoreBBRZ.Controllers.AdminController.adminListe);

            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine(ourJSONData);   
            }

            TempData["JSON"] = "Data saved!";
            return RedirectToAction("Index");
        }

        public ActionResult LoadJSON()
        {
            JavaScriptSerializer javascriptSerializer = new JavaScriptSerializer();
            string ourJSONData = null;

            if (!System.IO.File.Exists(filename))
                return View();

            using (StreamReader sw = new StreamReader(filename))
            {
                ourJSONData = sw.ReadToEnd();
            }

            dynamic listeVonAdmins = javascriptSerializer.Deserialize<List<Admin>>(ourJSONData);
            WebAppCoreBBRZ.Controllers.AdminController.adminListe = listeVonAdmins;

            TempData["JSON"] = "Data loaded -> Count: " + WebAppCoreBBRZ.Controllers.AdminController.adminListe.Count;
            return RedirectToAction("Index");
        }



        // GET: PersistenceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersistenceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersistenceController/Create
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

        // GET: PersistenceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersistenceController/Edit/5
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

        // GET: PersistenceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersistenceController/Delete/5
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
