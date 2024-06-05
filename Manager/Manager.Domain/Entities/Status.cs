using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain.Entities
{
    public class Status
    {
        public ObjectId Id { get; set; }
        public string TaskStatus { get; set; }
        public Status() { }
        public Status(string taskStatus)
        {
            TaskStatus = taskStatus;
        }
    }
}
