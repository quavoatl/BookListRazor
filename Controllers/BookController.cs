using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _db;

        public BookController(ApplicationDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return  Json(new { data = await _db.Book.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDB = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);

            if (bookFromDB ==null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _db.Book.Remove(bookFromDB);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete succesful" });

        }
    }
}