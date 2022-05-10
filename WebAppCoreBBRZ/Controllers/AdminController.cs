using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAppCoreBBRZ.Models;
using WebAppCoreBBRZ.ViewModels;

namespace WebAppCoreBBRZ.Controllers
{
    public class AdminController : Controller
    {
        private string salt = "unser Salz";
        public static List<Admin> adminListe = new List<Admin>();
        
        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        private static string GenerateHash(string pw)
        {
            var md5 = new MD5CryptoServiceProvider();
            var buf = Encoding.UTF8.GetBytes(pw);
            var md5data = md5.ComputeHash(buf, 0, buf.Length);

            return Encoding.UTF8.GetString(md5data);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RegisterVM registerVM)
        {
            Services.PersistenceService.LoadJSON();
            try
            {
                // To enable Data Annotation checks 
                if (!ModelState.IsValid)
                    return View();

                
                // Salzen und Pfeffern
                var pepper = registerVM.Email;
                var unserPW = registerVM.Password + salt + pepper;
                var hashedPW = GenerateHash(unserPW);

                // Alt kleiner 18 Jahre -> "Nur mit Erziehungsberechtigten registrierbar."
                if (registerVM.Alter < 18)
                {
                    ViewBag.Unter18 = "Nur mit Erziehungsberechtigten registrierbar.";

                    var result = View();
                    return result;
                }

                var numberOfUsersRegistered =
                    adminListe.Count();

                // Eintragen in die DB
                var user = new Admin();

                user.Id = adminListe.Count + 1;
                user.Name = registerVM.Name;
                user.Email = registerVM.Email;
                user.PW = hashedPW;

                if (numberOfUsersRegistered == 0)
                    user.Role = RoleType.Admin;
                else
                    user.Role = RoleType.User;

                adminListe.Add(user);

                Services.PersistenceService.SaveJSON();

                return RedirectToAction("Index2");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Index2()
        {
            return View();
        }

        // Liste aller User, die angelegt wurden: 
        public ActionResult Index3()  

        // WACB#00003 
        {

            Services.PersistenceService.LoadJSON();

            if (AmILoggedInternal() != null)
            {
                
                // WACB#00006
                
                string sessionID = null;

                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SessionId")))
                    sessionID = HttpContext.Session.GetString("SessionId");

                var user = adminListe.Where(x => x.SessionID == sessionID).FirstOrDefault();
                if (user == null)
                    return RedirectToAction("Index4", "Admin");

                var whoami = user.Role;

                if (whoami == RoleType.LoggingOut)
                {
                    return RedirectToAction("Index4", "Admin");
                }
                else if (whoami == RoleType.User)
                {

                    return View(adminListe.Where(x => x.SessionID == sessionID).ToList());
                }

                return View(adminListe.Where(x => x.Active == true).ToList());
            }
            else
            {
                return RedirectToAction("Index4", "Admin");
            }
        }

        public ActionResult Index4()
        {
            return View();
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            Services.PersistenceService.LoadJSON();

            TempData["tempid"] = id;
            return RedirectToAction("DetailsPartial", "Admin");

            // WACB#00002 
            {
                if (AmILoggedInternal() != null)
                {
                    var elemToDisplay = adminListe.Where(x => x.Id == id).FirstOrDefault();

                    return View(elemToDisplay);
                }
                else
                {
                    return RedirectToAction("Index4", "Admin");
                }
            }

            
        }

        public ActionResult DetailsPartial()
        {
            var id = TempData["tempid"];

            // WACB#00002 
            {
                if (AmILoggedInternal() != null)
                {
                    var elemToDisplay = adminListe.Where(x => x.Id.ToString() == id.ToString())
                        .FirstOrDefault();

                    return View(elemToDisplay);
                }
                else
                {
                    return RedirectToAction("Index4", "Admin");
                }
            }
        }

        //public static void resetLoggedUser()
        //{
        //    HttpContext.Session.SetString("SessionId", "" );
        //    HttpContext.Session.SetString("UserId", "" );
        //}

        // GET: AdminController/Create
        public ActionResult Login()
        {
            Services.PersistenceService.LoadJSON();
            return View();
        }

       

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(RegisterVM registerVM)
        {
            Services.PersistenceService.LoadJSON();

            try
            {
                // enable data annotation checks
                //if (!ModelState.IsValid)
                //    return View();

                if (registerVM.Email == null)
                {
                    ViewData["LoginError"] = "Please enter a valid email address!";
                    return View();

                }

                if (registerVM.Password == null)
                {
                    ViewData["LoginError"] = "Please enter a valid password!";
                    return View();

                }

                // Salzen und Pfeffern
                var pepper = registerVM.Email;
                var unserPW = registerVM.Password + salt + pepper;
                var hashedPW = GenerateHash(unserPW);

                var user = adminListe.Where(x => x.Email == registerVM.Email).FirstOrDefault();

                if (user == null)
                {
                    ViewData["LoginError"] = "Please enter valid credentials!";
                    return View();

                }
                
                if (user.PW != hashedPW)
                {
                    ViewData["LoginError"] = "Please enter valid credentials!";
                    return View();

                }

                HttpContext.Session.SetString("SessionId", HttpContext.Session.Id);
                HttpContext.Session.SetString("UserId", user.Id.ToString());

                user.SessionID = HttpContext.Session.Id;
                Services.PersistenceService.SaveJSON();

                if (AmILoggedInternal() == null)
                {
                    //Log me out
                    ;
                }

               

                return RedirectToAction(nameof(LoginSuccessful),"Admin");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Services.PersistenceService.LoadJSON();
            var userid = HttpContext.Session.GetString("UserId");


            // WACB#00001
            // userid not equal "" not asked -> AND inquiry with userid != ""
            if (userid != null && userid != "")
            {
                var user = adminListe.FirstOrDefault(x => x.Id == Int32.Parse(userid));
                user.SessionID = null;

            }

            HttpContext.Session.SetString("SessionId", "");
            HttpContext.Session.SetString("UserId", "");
            TempData["loggedin"] = null;
            Services.PersistenceService.SaveJSON();

            return View();
        }

        public static int GetNumberOfUsers()
        {
            return adminListe.Count;
        }


        /// <summary>
        /// AmILoggedInternal shows who I am at this moment (logged in as) 
        /// </summary>
        /// <returns>a User, if successful / null, if not logged in ... </returns>
        private Admin AmILoggedInternal()
        {
            Services.PersistenceService.LoadJSON();
            string sessionID = null;
            string userId = null;

            //if (TempData.Peek("loggedin") != null)
            //{
                try
                {
                    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SessionId")))
                        sessionID = HttpContext.Session.GetString("SessionId");

                    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                        userId = HttpContext.Session.GetString("UserId");

                }
                catch
                {
                    //sessionID = null;
                    //userId = null;
                    ;
                }
            //}

            


            // WACB#00003 
            // Proove, if Session is not null && SessionId actual is not equal to the remebered
            if (sessionID == null || sessionID != HttpContext.Session.Id)
                return null;

            else
            {
                TempData["loggedin"] = adminListe.FirstOrDefault(x => x.Id == Int32.Parse(userId)).Name;
                TempData.Keep("loggedin");

                // User session is still valid -> good! 
                return adminListe.FirstOrDefault(x => x.Id == Int32.Parse(userId));

            }

        }


        public ActionResult LoginSuccessful()
        {
            return View();
        }

        //// GET: AdminController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AdminController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            Services.PersistenceService.LoadJSON();

            // WACB#00002 
            {
                
                dynamic me = AmILoggedInternal();


                if (me != null)
                {
                    var elemToEdit = adminListe.Where(x => x.Id == id).FirstOrDefault();
                    ViewData["me"] = me;
                    return View(elemToEdit);
                }
                else
                {
                    return RedirectToAction("Index4", "Admin");
                }
            }

            
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Admin admin)
        {
            Services.PersistenceService.LoadJSON();
            try
            {
                var me = AmILoggedInternal();
                var elemToEdit = adminListe.Where(x => x.Id == id).FirstOrDefault();

                
                
                elemToEdit.Name = admin.Name;
                elemToEdit.Email = admin.Email;

                if (admin.Role == RoleType.LoggingOut)
                    elemToEdit.SessionID = null;
                else
                {
                    if (me != null)
                        if (me.Role == RoleType.Admin)
                        {
                            elemToEdit.Role = admin.Role;
                        }
                }

                Services.PersistenceService.SaveJSON();

                return RedirectToAction(nameof(Index3));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            Services.PersistenceService.LoadJSON();
            // WACB#00002 
            {
                if (AmILoggedInternal() != null)
                {
                    var elemToArchive = adminListe.Where(x => x.Id == id).FirstOrDefault();
                    return View(elemToArchive);
                }
                else
                {
                    return RedirectToAction("Index4", "Admin");
                }
            }

           
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Services.PersistenceService.LoadJSON();
            try
            {
                var elemToArchive = adminListe.Where(x => x.Id == id).FirstOrDefault();
                elemToArchive.Active = false;
                Services.PersistenceService.SaveJSON();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
