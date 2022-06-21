using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Entity;

namespace projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Books1Controller : ControllerBase
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;


        public Books1Controller(LibraryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Books1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            return await _context.Books.Include(c => c.Category).Include(c=>c.Authors).ToListAsync();
        }

        // GET: api/Books1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            var book = await _context.Books.Include(c=>c.Category).Include(c=>c.Authors).FirstOrDefaultAsync(c=>c.Id==id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, UpdateBookDto book)
        {
            var mapped= _mapper.Map<Book>(book);
            if (id != mapped.Id)
            {
                return BadRequest();
            }

            _context.Entry(mapped).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookDto book)
        {
          if (_context.Books == null)
          {
              return Problem("Entity set 'LibraryDbContext.Books'  is null.");
          }
          var mapped=_mapper.Map<Book>(book);

            _context.Books.Add(mapped);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = mapped.Id }, mapped);
        }

        // DELETE: api/Books1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
