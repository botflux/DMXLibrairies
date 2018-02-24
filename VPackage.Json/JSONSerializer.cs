using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace VPackage.Json
{
    /// <summary>
    /// Gestionnaire de sérialisation JSON
    /// </summary>
    public static class JSONSerializer
    {
        /// <summary>
        /// Sérialise l'instance de l'objet
        /// </summary>
        /// <typeparam name="T">Le type de l'objet à sérialiser</typeparam>
        /// <param name="o">Objet a sérialiser</param>
        /// <returns>L'objet sous forme de trame JSON</returns>
        /// <exception cref="AttributeMissingException">Lever lors ce que le type renseigné n'implémente pas DataContractAttribute</exception>
        /// <exception cref="QuotaExceededException">Lever lors ce que l'objet à sérialiser est trop volumineux</exception>
        public static string Serialize<T>(T o) where T : new ()
        {
            Attribute attrs = Attribute.GetCustomAttribute(typeof(T), typeof(DataContractAttribute));

            if (attrs == null)
                throw new AttributeMissingException("La classe spécifié n'implémente pas DataConctract");

            try
            {
                MemoryStream ms = new MemoryStream();

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(ms, o);
                byte[] json = ms.ToArray();
                ms.Close();
                return Encoding.UTF8.GetString(json, 0, json.Length);
            }
            catch (QuotaExceededException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Déserialise le chaîne de caractère
        /// </summary>
        /// <typeparam name="T">Le type d'objet à désérialiser</typeparam>
        /// <param name="content">Chaîne à déserialiser</param>
        /// <returns>L'objet désérialisé</returns>
        /// <exception cref="AttributeMissingException">Lever lors ce que le type renseigné n'implémente pas DataContractAttribute</exception>
        public static T Deserialize<T> (string content) where T : new()
        {
            Attribute attrs = Attribute.GetCustomAttribute(typeof(T), typeof(DataContractAttribute));

            if (attrs == null)
                throw new AttributeMissingException("La classe spécifié n'implémente pas DataConctract");

            try
            {
                T o = new T();
                MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(content));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(o.GetType());
                o = (T)ser.ReadObject(ms);
                ms.Close();
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
