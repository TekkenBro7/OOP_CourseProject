using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain.Entities
{
    public class Comment
    {
        public ObjectId Id { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public Comment() { }
        public Comment(string content)
        {
            Content = content;
            DateTime = DateTime.Now;
        }
    }
}
