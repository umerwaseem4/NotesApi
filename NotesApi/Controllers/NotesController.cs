using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApi.Data;
using NotesApi.Model;

namespace NotesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly NotesApiDbContext dbContext;

        public NotesController(NotesApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotes()
        {
            return Ok(await dbContext.Notes.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(AddNotesResult addNotesResult)
        {
            var note = new Notes()
            {
                Id = Guid.NewGuid(),
                Name = addNotesResult.Name,
                Status = addNotesResult.status
            };

            dbContext.Notes.Add(note);
            dbContext.SaveChanges();
            return Ok(note);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetNote([FromRoute] Guid id)
        {
            var note = await dbContext.Notes.FindAsync(id);
            if(note == null)
            {
                NotFound();
            }

            return Ok(note);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, NotesApiUpdateRequest notesApiUpdateRequest)
        {
            var contact = await dbContext.Notes.FindAsync();
            if(contact != null)
            {
                contact.Name = notesApiUpdateRequest.Name;
                contact.Status = notesApiUpdateRequest.Status;
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            var note = await dbContext.Notes.FindAsync(id);
            if(note != null)
            {
                dbContext.Remove(note);
                await dbContext.SaveChangesAsync();
                return Ok(note);
            }
            return NotFound();
        }

    }
}
