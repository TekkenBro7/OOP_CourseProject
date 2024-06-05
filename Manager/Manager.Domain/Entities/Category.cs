using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain.Entities
{
    public class Category
    {
        public ObjectId Id { get; set; }
        public string TaskCategory { get; set; }
        public Category() { }
        public Category(string taskCategory)
        {
            TaskCategory = taskCategory;
        }
    }
}
