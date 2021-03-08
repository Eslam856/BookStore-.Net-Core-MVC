using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class AuthorRepository : IBookStoreRepository<Author>
    {
        List<Author> Authors;
        public AuthorRepository()
        {
            Authors = new List<Author>()
            {
                new Author{Id=0,AuthorName="---Please Select Author--------"},
                new Author{Id=1,AuthorName="Udacity by Mosh"},
                new Author{Id=2,AuthorName="Mohamed Emad Arafa"},
                new Author{Id=3,AuthorName="Coursera By Andrew"},
                new Author{Id=4,AuthorName="Khaled Elsadany"},
                new Author{Id=5,AuthorName="Eslam Muhamed"}

            };

        }

        public void Add(Author author)
        {
            author.Id = Authors.Max(a => a.Id) + 1;
            Authors.Add(author);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            Authors.Remove(author);
        }

        public Author Find(int id)
        {
          var author=  Authors.SingleOrDefault(a => a.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            return Authors;
        }

        public void Update(int id, Author newauthor)
        {
            var author = Find(id);
            author.AuthorName = newauthor.AuthorName;

        }
    }
}
