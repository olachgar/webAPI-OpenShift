using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotebookAppApi.Model
{
    public class Note
    {
        [BsonId]
        public ObjectId InternalId { get; set; }

        public string Id { get; set; }

        public string Body { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public int UserId { get; set; } = 0;
    }
}