using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotebookAppApi.Model;
using WebApi_Core2_Test01.Model;

namespace WebApi_Core2_Test01.Controllers
{
    // inspired from https://github.com/fpetru/WebApiMongoDB
    // http://www.qappdesign.com/using-mongodb-with-net-core-webapi/

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        // Note Repository
        private readonly INoteRepository _noteRepository;


        // Default constructor
        public NotesController(INoteRepository noteRepository){
            this._noteRepository = noteRepository;
        }


        // GET all nots 
        // URI GET api/notes
        [HttpGet]
        public async Task<IEnumerable<Note>> Get()
        {
            return await this._noteRepository.GetAllNotes();
        }


        // Get one Note
        // URI GET api/notes/5       
        [HttpGet("{id}")]
        public async Task<Note> Get(string id)
        {
            //
            return await this._noteRepository.GetNote(id) ?? new Note();
        }


        // POST api/notes
        [HttpPost]
        public void Post([FromBody] NoteParam newNote)
        {
            this._noteRepository.AddNote(new Note {});
        }

        // PUT api/notes/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            this._noteRepository.UpdateNote(id, value);
        }

        // DELETE api/notes/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            this._noteRepository.RemoveNote(id);
        }
    }
}
