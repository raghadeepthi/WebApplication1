using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebApplication1.ExternalModels;

namespace WebApplication1.Services
{
    public interface IBooksRepository
    {
      Task<IEnumerable<Entities.Book>> GetBooksAsync();
      Task<Entities.Book>  GetBookAsync(Guid id);

        void AddBook(Book booktoAdd);

        Task<bool> saveChangesAsyn();
        Task<IEnumerable<Entities.Book>> GetBookCollectionAsync(IEnumerable<Guid> BookIds);
        Task<BookCover> GetBookCoverAsync(string coverId);

        Task<IEnumerable<BookCover>> GetBookCoversAsync(Guid BookId);
    }
}
