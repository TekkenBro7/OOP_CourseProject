using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace SerializerLib
{
    public class UserSerializer<T> : ISerializer<T>
    {
        public List<T> DeSerializeJSON(string fileName)
        {
            try
            {
                string jsonString = File.ReadAllText(fileName);
                List<T> list = JsonSerializer.Deserialize<List<T>>(jsonString);
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при десериализации JSON: {ex.Message}");
                return new List<T>();
            }
        }

        public List<T> DeSerializeXML(string fileName)
        {
            try
            {
                XmlSerializer xmlSerializer = new(typeof(List<T>));
                using FileStream stream = new(fileName, FileMode.OpenOrCreate);
                List<T> list = (List<T>)xmlSerializer.Deserialize(stream);
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при десериализации XML: {ex.Message}");
                return new List<T>();
            }
        }

        public void SerializeJSON(IEnumerable<T> objects, string fileName)
        {
            string jsonString = JsonSerializer.Serialize(objects);
            File.WriteAllText(fileName, jsonString);
        }

        public void SerializeXML(IEnumerable<T> objects, string fileName)
        {
            if (File.Exists(fileName))
            {
                File.WriteAllText(fileName, string.Empty);
            }
            XmlSerializer xmlSerializer = new(typeof(List<T>));
            using var stream = new FileStream(fileName, FileMode.OpenOrCreate);
            xmlSerializer.Serialize(stream, objects);
        }
    }
}
