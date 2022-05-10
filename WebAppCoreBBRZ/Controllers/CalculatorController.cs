using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreBBRZ.Models;

namespace WebAppCoreBBRZ.Controllers
{
    public class CalculatorController : Controller
    {
        public static List<Calculator> calcListe = new List<Calculator>();



        /// <summary>
        /// Initializes the certain calcListe element
        /// by reserving a user space with the key: SessionID.
        /// 
        /// So the user should only access its own results.
        /// 
        /// <seealso href="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-5.0"/>
        /// </summary>
        /// <returns></returns>
        // GET: CalculatorController
        public ActionResult Index()
        {
            // Purpose: If you set one variable, the session ID will remain until expired. 
            // Note: If you do not set one variable, it will change each recall ... bug?? 

            HttpContext.Session.SetString("Something", "Something");


            // User is not in List ... with SessionID 
            if (calcListe.FirstOrDefault(x => x.SessionID == HttpContext.Session.Id) == null)
            {
                calcListe.Add(new Calculator()
                {
                    Id = calcListe.Count + 1,
                    SessionID = HttpContext.Session.Id
                });
            }


            return RedirectToAction("Calc");
        }

        // GET: CalculatorController
        public ActionResult Calc(string value, string errmsg)
        {
            // Bring errmsg to View: 
            ViewBag.errmsg = errmsg;


            var currentElement = calcListe.FirstOrDefault(x => x.SessionID == HttpContext.Session.Id);

            if (currentElement == null)
            {
                return RedirectToAction("Index");
            }

            int resultInt;
            

            if (value != null)
            {
                //if (value == "0")
                //{
                //    var tempString = currentElement.Register1.ToString();
                //    tempString += value;
                //    currentElement.Register1 = tempString;

                //}
                //else
                
                if (Int32.TryParse(value,out resultInt) )
                {
                    if (currentElement.Register1.ToString() == "0")
                        currentElement.Register1 = "";

                    var tempString = currentElement.Register1.ToString();
                    tempString += value;
                    currentElement.Register1 = tempString;
                    
                }
                else if (value == "C")
                {
                    currentElement.Register1 = "0";
                }
                else if (value == ".")
                {
                    if (!currentElement.Register1.Contains(","))
                        currentElement.Register1 += ",";
                }

                else if (value == "AC")
                {
                    currentElement.Results[0] = 0.0;
                    currentElement.Results[1] = 0.0;
                    currentElement.Results[2] = 0.0;
                    currentElement.Register1 = "0";
                }

                else if (value == "SUM")
                {
                    currentElement.Register1 = (currentElement.Results[2]
                        + currentElement.Results[1]
                        + currentElement.Results[0]
                        + double.Parse(currentElement.Register1)).ToString();
                    currentElement.Results[0] = 0.0;
                    currentElement.Results[1] = 0.0;
                    currentElement.Results[2] = 0.0;
                    
                }

                else if (value == "AVG")
                {
                    currentElement.Register1 = ((currentElement.Results[2]
                        + currentElement.Results[1]
                        + currentElement.Results[0]
                        + double.Parse(currentElement.Register1)) / 4).ToString();
                    currentElement.Results[0] = 0.0;
                    currentElement.Results[1] = 0.0;
                    currentElement.Results[2] = 0.0;

                }


                else if (value == "Enter")
                {
                    currentElement.Results[0] = currentElement.Results[1];
                    currentElement.Results[1] = currentElement.Results[2];
                    currentElement.Results[2] = double.Parse(currentElement.Register1);
                    currentElement.Register1 = "0";
                }

                else if (value == "+")
                {
                    // 0 -> R4

                    currentElement.Register1 = (double.Parse(currentElement.Register1) +
                        currentElement.Results[2]).ToString();
                    currentElement.Results[2] = currentElement.Results[1];
                    currentElement.Results[1] = currentElement.Results[0];
                    currentElement.Results[0] = 0;
                }
                else if (value == "-")
                {
                    // 0 -> R4

                    currentElement.Register1 = (currentElement.Results[2] -
                        double.Parse(currentElement.Register1)).ToString() ;
                    currentElement.Results[2] = currentElement.Results[1];
                    currentElement.Results[1] = currentElement.Results[0];
                    currentElement.Results[0] = 0;
                }
                else if (value == "/")
                {
                    // 0 -> R4

                    // Divide by zero? 

                    if (double.Parse(currentElement.Register1) == 0)
                    {
                        return RedirectToAction("CalcError");

                    }

                    currentElement.Register1 = (currentElement.Results[2] /
                        double.Parse(currentElement.Register1)).ToString();
                    currentElement.Results[2] = currentElement.Results[1];
                    currentElement.Results[1] = currentElement.Results[0];
                    currentElement.Results[0] = 0;

                   

                }
                else if (value == "*")
                {
                    // 0 -> R4

                    currentElement.Register1 = (currentElement.Results[2] *
                       double.Parse(currentElement.Register1)).ToString();
                    currentElement.Results[2] = currentElement.Results[1];
                    currentElement.Results[1] = currentElement.Results[0];
                    currentElement.Results[0] = 0;
                }


            }

            return View(calcListe.FirstOrDefault(x => x.SessionID == HttpContext.Session.Id));
        }

        // GET: CalculatorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult CalcError()
        {
            return View();
        }

        // GET: CalculatorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CalculatorController/Create
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

        // GET: CalculatorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CalculatorController/Edit/5
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

        // GET: CalculatorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CalculatorController/Delete/5
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
