using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain
{
    public class Status
    {
        public int Id { get; private set; }
        public string TaskStatus { get; private set; }
        public Status(string taskStatus)
        {
            TaskStatus = taskStatus;
        }
    }
}
