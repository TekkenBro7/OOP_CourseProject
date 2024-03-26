using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain
{
    public class Comment
    {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public DateTime DateTime { get; private set; }
        public Comment(string content)
        {
            Content = content;
            DateTime = DateTime.Now;
        }
    }
}
