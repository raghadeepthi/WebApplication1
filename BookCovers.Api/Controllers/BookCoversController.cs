using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCovers.Api.Controllers
{
    [ApiController]
    [Route("api/bookcovers")]
    public class BookCoversController : ControllerBase
    {
        [HttpGet("{name}")]
        public async Task<IActionResult> GetBookCover(string name,bool returnfault = false)
        {
            if(returnfault)

            {
                await Task.Delay(500);
                return new StatusCodeResult(500);
            }

            var random = new Random();
            int fakeCoverbytes = random.Next(2097152, 10485760);
            byte[] fakecover = new byte[fakeCoverbytes];
            random.NextBytes(fakecover);
            return Ok(new
            {
                Name = name,
                Content = fakecover
            });



        }
    }
}
