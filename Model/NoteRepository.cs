using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace NotebookAppApi.Model
{
    public class NoteRepository : INoteRepository
    {
        private readonly DBContext _context = null;

        // Default constructor for Note Repository
        public NoteRepository(IOptions<Settings> settings)
        {
            this._context = new DBContext(settings);
        }

        public async Task AddNote(Note item)
        {
            try
            {
                await this._context
                          .Notes
                          .InsertOneAsync(item);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            try
            {
                return await this._context
                                 .Notes
                                 .Find(_ => true)
                                 .ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Note> GetNote(string id)
        {
            try
            {
                return await this._context
                                 .Notes
                                 .Find(x => x.Id.Equals(id))
                                 .FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveAllNotes()
        {
            try
            {
                DeleteResult deleteResult = await this._context
                                                      .Notes
                                                      .DeleteManyAsync(new BsonDocument());
                
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveNote(string id)
        {
            var filter = Builders<Note>.Filter.Eq("Id", id);

            try
            {
                DeleteResult deleteResult = await this._context
                                                      .Notes
                                                      .DeleteOneAsync(filter);

                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateNote(string id, string body)
        {
            var filter = Builders<Note>.Filter.Eq("Id", id);
            var update = Builders<Note>.Update
                                       .Set(x => x.Body, body)
                                       .CurrentDate(s => s.UpdatedOn);
            try
            {
                UpdateResult updateResult = await this._context
                                                      .Notes
                                                      .UpdateOneAsync(filter,update);
                
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> UpdateNoteDocument(string id, string body)
        {
            throw new System.NotImplementedException();
        }
    }
}