using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Context;
using WebApplication1.Entities;
using WebApplication1.ExternalModels;
 
namespace WebApplication1.Services
{
    public class BooksRepository : IBooksRepository , IDisposable
    {
        private BooksDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private CancellationTokenSource _cancellationTokenSource;
         
 
        public BooksRepository(BooksDbContext context,IHttpClientFactory httpClientFactory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
             //_cancellationTokenSource = cancellationTokenSource ?? throw new ArgumentException(nameof(cancellationTokenSource));
        }
        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
          return  await _context.Books.Include(a => a.Author).ToListAsync();
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            return await _context.Books.Include(a => a.Author).FirstOrDefaultAsync(b => b.id == id);
        }

         public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool Disposing)
        {
            if(Disposing)
            {
                if(_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
                if (_cancellationTokenSource != null)
                {
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = null;
                }
            }
        }

        public void AddBook(Book booktoAdd)
        {
            if(booktoAdd == null)
            {
                throw new ArgumentNullException(nameof(booktoAdd));
            }
            _context.Add(booktoAdd);
        }

        public async Task<bool> saveChangesAsyn()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<IEnumerable<Book>> GetBookCollectionAsync(IEnumerable<Guid> BookIds)
        {
           return  await _context.Books.Where(b => BookIds.Contains(b.id)).Include(a => a.Author).ToListAsync();
        }

        public async Task<BookCover> GetBookCoverAsync(string coverId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:44306/api/bookcovers/{coverId}");

            if(response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BookCover>(await response.Content.ReadAsStringAsync());

            }
            return null;


        }

        public async Task<IEnumerable<BookCover>> GetBookCoversAsync(Guid bookId )
        {
            var httpClient = _httpClientFactory.CreateClient();
            var bookCovers = new List<BookCover>();
            _cancellationTokenSource = new CancellationTokenSource();
             var bookCoversUrls = new[]
            {
                $"https://localhost:44306/api/bookcovers/{bookId}-dummyCover1",
                $"https://localhost:44306/api/bookcovers/{bookId}-dummyCover2?returnfault=true",
                $"https://localhost:44306/api/bookcovers/{bookId}-dummyCover3",
                $"https://localhost:44306/api/bookcovers/{bookId}-dummyCover4",
                $"https://localhost:44306/api/bookcovers/{bookId}-dummyCover5",

            };

            var downloadBookCoverQuery =
                from downloadbookCover
                in bookCoversUrls
                select DownloadBookCover(httpClient, downloadbookCover, _cancellationTokenSource.Token);

            var downloadBookCoverTask = downloadBookCoverQuery.ToList();

            return await Task.WhenAll(downloadBookCoverTask);

         }

        public async Task<BookCover> DownloadBookCover(HttpClient httpClient,string bookCoverUrl,CancellationToken cancellationToken)
        {
            var response = await httpClient.GetAsync(bookCoverUrl);
            if (response.IsSuccessStatusCode)
            {
                var bookCover = JsonConvert.DeserializeObject<BookCover>(await response.Content.ReadAsStringAsync());
                return bookCover;

            }
            _cancellationTokenSource.Cancel();
            return null;


        }

       
        
    
    }
}
