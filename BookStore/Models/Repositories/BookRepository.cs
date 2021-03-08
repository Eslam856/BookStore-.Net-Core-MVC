using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class BookRepository : IBookStoreRepository<Book>
    {
        List<Book> Books;
       public BookRepository ()
        {
            Books = new List<Book>()
            {
                new Book{Id=1,Title="c#",Description="Learning Basics For Beginner",ImageURL="cSharp.png" ,Author=new Author(){Id=1,AuthorName="Udacity by Mosh"} },
                new Book{Id=2,Title=".Net Core",Description="Back End Using .Net Core", ImageURL="Core.png" ,Author=new Author(){Id=2,AuthorName="Mohamed Emad Arafa"}},
                new Book{Id=3,Title="Angular8",Description="Learning Angular by Army", ImageURL="angular.png" ,Author=new Author(){Id=3,AuthorName="Coursera By Andrew"}},
                new Book{Id=4,Title="Front End",Description="Html5,Css3,Java Script,Bootstrap",ImageURL="html.png" ,Author=new Author(){Id=4,AuthorName="Tomath"}},

               new Book{Id=5,Title="c#",Description="Learning Basics For Beginner",ImageURL="cSharp.png" ,Author=new Author(){Id=1,AuthorName="Udacity by Mosh"} },
                new Book{Id=6,Title=".Net Core",Description="Back End Using .Net Core", ImageURL="Core.png" ,Author=new Author(){Id=2,AuthorName="Mohamed Emad Arafa"}},
                new Book{Id=7,Title="Angular8",Description="Learning Angular by Army", ImageURL="angular.png" ,Author=new Author(){Id=3,AuthorName="Coursera By Andrew"}},
                new Book{Id=8,Title="Front End",Description="Html5,Css3,Java Script,Bootstrap",ImageURL="html.png" ,Author=new Author(){Id=4,AuthorName="Tomath"}},

                new Book{Id=9,Title="c#",Description="Learning Basics For Beginner",ImageURL="cSharp.png" ,Author=new Author(){Id=1,AuthorName="Udacity by Mosh"} },
                new Book{Id=10,Title=".Net Core",Description="Back End Using .Net Core", ImageURL="Core.png" ,Author=new Author(){Id=2,AuthorName="Mohamed Emad Arafa"}},
                new Book{Id=11,Title="Angular8",Description="Learning Angular by Army", ImageURL="angular.png" ,Author=new Author(){Id=3,AuthorName="Coursera By Andrew"}},
                new Book{Id=12,Title="Front End",Description="Html5,Css3,Java Script,Bootstrap",ImageURL="html.png" ,Author=new Author(){Id=4,AuthorName="Tomath"}}

            };
        }

        public void Add(Book book)
        {
            book.Id = Books.Max(b => b.Id) + 1;
            Books.Add(book);
        }

        public void Delete(int id)
        {
            var book=Find(id);
            Books.Remove(book);
            
        }

        public Book Find(int id)
        {
           var book= Books.SingleOrDefault(b => b.Id==id);
            return book;
        }

        public IList<Book> List()
        {
            return Books;
        }

        public void Update(int id,Book newbook)
        {
            var book = Find(id);
            book.Title = newbook.Title;
            book.Description = newbook.Description;
            book.Author = newbook.Author;
            book.ImageURL = newbook.ImageURL;
             
        }
    }
}
