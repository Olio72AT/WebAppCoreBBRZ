using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAppCoreBBRZ.Controllers
{
    public class JavascriptController : Controller
    {
        // GET: JavascriptController
        public ActionResult Index()
        {
            // Now write the current time to the cookie

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(2);
            Response.Cookies.Append("Snipplets", DateTime.Now.ToString(), option);


            //HttpCookie cookienew = new HttpCookie("todoAPP");

            //cookienew.Value = "Today is a good day ... now change me to an JSON string ánd try it again ;)";
            //cookienew.Expires = DateTime.Now.AddHours(1);

            //HttpContext.Current.Response.SetCookie(cookienew);

            return View();
        }

        // GET: JavascriptController
        public ActionResult Index2()
        {
            // Now write the current time to the cookie

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(2);
            Response.Cookies.Append("Snipplets", DateTime.Now.ToString(), option);


            //HttpCookie cookienew = new HttpCookie("todoAPP");

            //cookienew.Value = "Today is a good day ... now change me to an JSON string ánd try it again ;)";
            //cookienew.Expires = DateTime.Now.AddHours(1);

            //HttpContext.Current.Response.SetCookie(cookienew);

            return View();
        }

        // GET: JavascriptController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JavascriptController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JavascriptController/Create
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

        // GET: JavascriptController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JavascriptController/Edit/5
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

        // GET: JavascriptController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JavascriptController/Delete/5
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
