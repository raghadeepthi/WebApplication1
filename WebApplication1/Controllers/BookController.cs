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
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private IBooksRepository _bookRepository { get; set; }
        private   IMapper _mapper {get;set;}
        public BookController(IBooksRepository booksRepository, IMapper mapper)
        {
            _bookRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [BooksResultFilterAttibute]
        public async Task<IActionResult> GetBooks()
        {
            var bookEntities = await _bookRepository.GetBooksAsync();
            return Ok(bookEntities);
        }

        [HttpGet]
        [Route("{id}",Name = "GetBook")]
        [BookWithCoversResultFilter]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var bookEntity = await _bookRepository.GetBookAsync(id);
            if (bookEntity == null)
                return NotFound();
            var bookCovers = await _bookRepository.GetBookCoversAsync(id);

             return Ok((bookEntity,bookCovers));
        }
 
        [HttpPost]
        [BookResultFilter]
        public async Task<IActionResult> CreateBook([FromBody] BookForCreation bookForCreation)
        {

          var bookToAdd =  _mapper.Map<Entities.Book>(bookForCreation);
            _bookRepository.AddBook(bookToAdd);
            await _bookRepository.saveChangesAsyn();
           await  _bookRepository.GetBookAsync(bookToAdd.id);

            return CreatedAtRoute("GetBook", new { id = bookToAdd.id }, bookToAdd);
        }
    }
}
