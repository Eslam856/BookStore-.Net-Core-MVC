using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{

    public class AuthorController : Controller
    {
       
        private readonly IBookStoreRepository<Author> authorRepository;

        public AuthorController(IBookStoreRepository<Author>authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        // GET: AuthorController
        public ActionResult Index()
        {
            var Authors = authorRepository.List();
            return View(Authors);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            var author = authorRepository.Find(id);
            return View(author);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    authorRepository.Add(author);

                    return RedirectToAction("Index", "Author");
                }
                catch (Exception)
                {

                    return View();
                }

            }

            ModelState.AddModelError("", "Please Fill All Required Fields !");
            return View();
           
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = authorRepository.Find(id);
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author newAuthor)
        {
            try
            {
                //var author = authorRepository.Find(id);
                //author.Id = newAuthor.Id;
                //author.AuthorName = newAuthor.AuthorName;

                authorRepository.Update(id,newAuthor);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        [HttpGet]

        public ActionResult Delete(int id)
        {
            var author = authorRepository.Find(id);
            return View(author);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Author author)
        {
            try
            {

                authorRepository.Delete(id);
               
               return RedirectToAction("Index", "Author");
            }
            catch (Exception)
            {

                return View();
            }
           
        }
    }
}
