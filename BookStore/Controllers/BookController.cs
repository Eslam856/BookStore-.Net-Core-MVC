using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Models.Repositories;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BookStore.Controllers
{
   
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRepository;
        private readonly IBookStoreRepository<Author> authorRepository;
        [Obsolete]
        private readonly IHostingEnvironment hosting;

        [Obsolete]
        public BookController(IBookStoreRepository<Book> bookRepository,IBookStoreRepository<Author> authorRepository ,IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }

        // GET: BookController
        public ActionResult Index()
        {
           var Books= bookRepository.List();
            return View(Books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var getAuthors = new BookAuthorViewModel
            {
                Authors = authorRepository.List().ToList()
            };
            return View(getAuthors);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Create(BookAuthorViewModel model)
        {
            var author = authorRepository.Find(model.AuthorId);


            if (ModelState.IsValid)
            {

                try
                {
                    string fileName = string.Empty;
                    if(model.File!=null)
                   {
                        string Uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                        fileName = model.File.FileName;
                        string FullPath = Path.Combine(Uploads, fileName);
                        model.File.CopyTo(new FileStream(FullPath, FileMode.Create));

                    }

                    if (model.AuthorId == 0)
                    {
                        ViewBag.Message = "Please Select Author !";

                        return View(getAuthors());
                    }

                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = author,
                        ImageURL=fileName
                    };
                    bookRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            
            ModelState.AddModelError(" ","Please Fill Required Fields !");
            return View(getAuthors());
                
            
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {

            var book = bookRepository.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var model = new BookAuthorViewModel
            {
                 BookId=book.Id,
                 Title=book.Title,
                 Description=book.Description,
                 AuthorId=authorId,
                ImageURL = book.ImageURL,
                Authors = authorRepository.List().ToList()
            };
            return View(model);
            
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Edit(BookAuthorViewModel viewModel)
        {
            if (viewModel.AuthorId == 0)
            {
                ViewBag.Message = "Please Select Author !";

                return View(getAuthors());
            }
            try
            {

                var author = authorRepository.Find(viewModel.AuthorId);
                string fileName = string.Empty;
                if (viewModel.File != null)
                {
                    string Uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                    fileName = viewModel.File.FileName;
                    string FullPath = Path.Combine(Uploads, fileName);

                    //**Delete Old File Path
                    string oldFileName = bookRepository.Find(viewModel.BookId).ImageURL;
                    string FullOldPath = Path.Combine(Uploads, oldFileName);
                     if(FullPath!=FullOldPath)
                    {
                        System.IO.File.Delete(FullOldPath);

                        //**Save New File Path
                        viewModel.File.CopyTo(new FileStream(FullPath, FileMode.Create));

                    }

                }
                Book book = new Book
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Author = author,
                    ImageURL=fileName
                };
                bookRepository.Update(viewModel.BookId,book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Book book)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
         BookAuthorViewModel  getAuthors()
            {
            var Vmodel = new BookAuthorViewModel
            {
                 Authors = authorRepository.List().ToList()
            };
            return Vmodel;
        }
}
}
