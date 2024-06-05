using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializerLib
{
    public interface ISerializer<T>
    {
        void SerializeXML(IEnumerable<T> users, string fileName);
        void SerializeJSON(IEnumerable<T> users, string fileName);
        List<T> DeSerializeXML(string fileName);
        List<T> DeSerializeJSON(string fileName);
    }
}
