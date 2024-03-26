using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain
{
    public class Priority
    {
        public int Id { get; private set; }
        public string PriorityLevel { get; private set; }
        public Priority(string priorityLevel)
        {
            PriorityLevel = priorityLevel;
        }
    }
}
