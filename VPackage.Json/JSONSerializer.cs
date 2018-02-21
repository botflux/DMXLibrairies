using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VPackage.Json
{
    public static class JSONSerializer
    {
        /// <summary>
        /// Sérialise l'instance de l'objet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">Objet a sérialiser</param>
        /// <returns></returns>
        public static string Serialize<T>(T o) where T : new ()
        {
            Attribute attrs = Attribute.GetCustomAttribute(typeof(T), typeof(DataContractAttribute));

            if (attrs == null)
                throw new Exception("La classe spécifié n'implémente pas DataConctract");

            MemoryStream ms = new MemoryStream();
            
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            ser.WriteObject(ms, o);
            byte[] json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);

        }

        /// <summary>
        /// Déserialise le chaîne de caractère
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content">Chaîne à déserialiser</param>
        /// <returns></returns>
        public static T Deserialize<T> (string content) where T : new()
        {
            T o = new T();
            MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(content));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(o.GetType());
            o = (T)ser.ReadObject(ms);
            ms.Close();
            return o;
        }
    }
}
