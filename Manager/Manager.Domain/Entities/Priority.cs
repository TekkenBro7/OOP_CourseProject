using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain.Entities
{
    public class Priority
    {
        public ObjectId Id { get; set; }
        public string PriorityLevel { get; set; }
        public Priority() { }
        public Priority(string priorityLevel)
        {
            PriorityLevel = priorityLevel;
        }
    }
}
