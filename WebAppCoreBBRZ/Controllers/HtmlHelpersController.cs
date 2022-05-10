using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreBBRZ.Models;

namespace WebAppCoreBBRZ.Controllers
{
    public class HtmlHelpersController : Controller
    {
        public static HtmlTagHelpers OneItem = new HtmlTagHelpers();
        public static List<string> LBTransfer = new List<string>();

        // GET: HtmlHelpersController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HTMLHelpers/Create
        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test(HtmlTagHelpers data)
        {
            // To enable Data Annotation checks 
            if (!ModelState.IsValid)
                return View();

            if (data.RepeatPin == null)
                return View();

            try
            {
                // TODO: Add insert logic here

                // Assign the given data to our storage
                // OneItem = data;

                TempData["OneItem"] = JsonConvert.SerializeObject(data);

                if (LBTransfer.Count() > 0)
                {
                    LBTransfer.Clear();

                }
                if (data.ListBox != null)
                    // Listbox Preparation
                    foreach (string x in data.ListBox)
                    {
                        LBTransfer.Add(x);

                    }

                return RedirectToAction("TestOutput");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult TestOutput()
        {
            return View(JsonConvert.DeserializeObject<HtmlTagHelpers>((string)TempData["OneItem"]));
        }


        // GET: HtmlHelpersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HtmlHelpersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HtmlHelpersController/Create
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

        // GET: HtmlHelpersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HtmlHelpersController/Edit/5
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

        // GET: HtmlHelpersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HtmlHelpersController/Delete/5
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
