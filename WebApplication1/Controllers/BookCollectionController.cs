using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Filters;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/bookscollection")]
    [BooksResultFilterAttibute]
    public class BookCollectionController : ControllerBase
    {
        private IBooksRepository _bookRepository { get; set; }
        private IMapper _mapper { get; set; }

        public BookCollectionController(IBooksRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{bookIds}", Name = "GetBooksCollection")]
       
        public async Task<IActionResult> GetBookCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> bookIds)
        {
           var bookEntities = await _bookRepository.GetBookCollectionAsync(bookIds);
            if (bookIds.Count() != bookEntities.Count())
                return NotFound();
            return Ok(bookEntities);
        }


        [HttpPost]
       
        public async Task<IActionResult> CreateBookCollection([FromBody] IEnumerable<BookForCreation> bookcollection)
        {
            var bookEntities = _mapper.Map<IEnumerable<Entities.Book>>(bookcollection);
            foreach(var book in bookEntities)
            {
                _bookRepository.AddBook(book);
            }
            await _bookRepository.saveChangesAsyn();


            var booksToReturn =  await _bookRepository.GetBookCollectionAsync(bookEntities.Select(b => b.id).ToList());
            var bookIds = string.Join(",", booksToReturn.Select(b => b.id));
             
            return CreatedAtRoute("GetBooksCollection", new { bookIds }, booksToReturn);

         }
    }
}
