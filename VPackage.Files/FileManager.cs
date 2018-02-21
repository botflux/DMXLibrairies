using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VPackage.Files
{
    public static class FileManager
    {
        public static void Write (string path, string name, string content)
        {
            if (path[path.Length - 1] == '\\')
                path = path.TrimEnd('\\');

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException(string.Format("Specified directory not existing: {0}", path));
            }
            
            string completePath = path + "\\" + name;

            try
            {
                File.WriteAllText(completePath, content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Read (string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(string.Format("File not found: {0}", path));
            }

            try
            {
                return File.ReadAllText(path);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
