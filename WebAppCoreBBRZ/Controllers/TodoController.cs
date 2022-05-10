using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreBBRZ.Models;

namespace WebAppCoreBBRZ.Controllers
{
    public class TodoController : Controller
    {
        public static List<Todo> todoListe = new List<Todo>();

        // GET: TodoController
        public ActionResult Index(string sortaufgabe, string sortbeschreibung, string sortdone, 
            bool? doneitem, int? id )
        {
            var elemToDisplay = todoListe.Where(x => x.Active == true).ToList();

            if (doneitem != null && id != null)
                if (doneitem == false)
                    todoListe.FirstOrDefault(x => x.Id == id).Done = true;
                else
                    todoListe.FirstOrDefault(x => x.Id == id).Done = false;

            if (sortdone == "up")
                return View(elemToDisplay.OrderBy(x => x.Done).ThenBy(x => x.Aufgabe ));
            else if (sortdone == "down")
                return View(elemToDisplay.OrderByDescending(x => x.Done).ThenBy(x => x.Aufgabe));

            if (sortbeschreibung == "up")
                return View(elemToDisplay.OrderBy(x => x.Beschreibung));
            else if (sortbeschreibung == "down")
                return View(elemToDisplay.OrderByDescending(x => x.Beschreibung));

            if (sortaufgabe == "up")
                return View(elemToDisplay.OrderBy(x => x.Aufgabe));
            else if (sortaufgabe == "down")
                return View(elemToDisplay.OrderByDescending(x => x.Aufgabe));
            else
                return View(elemToDisplay);
        }

        // GET: TodoController/Details/5
        public ActionResult Details(int id)
        {
            return View(todoListe.FirstOrDefault(y => y.Id == id));
        }

        // GET: TodoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Todo todo)
        {
            try
            {
                var itemToCreate = new Todo();
                itemToCreate.Id = todoListe.Count + 1;
                itemToCreate.Aufgabe = todo.Aufgabe;
                itemToCreate.Beschreibung = todo.Beschreibung;
                itemToCreate.Done = todo.Done;

                todoListe.Add(itemToCreate);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(todoListe.FirstOrDefault(y => y.Id == id));
        }

        // POST: TodoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Todo todo)
        {
            try
            {
                var elemToEdit = todoListe.FirstOrDefault(y => y.Id == id);
                
                elemToEdit.Aufgabe = todo.Aufgabe;
                elemToEdit.Beschreibung = todo.Beschreibung;
                elemToEdit.Done = todo.Done;
                elemToEdit.FKUser = todo.FKUser;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(todoListe.FirstOrDefault(x => x.Id == id));
        }

        // POST: TodoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                todoListe.FirstOrDefault(x => x.Id == id).Active = false;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
