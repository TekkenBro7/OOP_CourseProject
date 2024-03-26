using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain
{
    public class Category
    {
        public int Id { get; private set; }
        public string TaskCategory { get; private set; }
        public Category(string taskCategory)
        {
            TaskCategory = taskCategory;
        }
    }
}
