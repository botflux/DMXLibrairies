using System;
using System.IO;
using System.Security;

namespace VPackage.Files
{
    /// <summary>
    /// Gestionnaire de fichiers
    /// </summary>
    public static class FileManager
    {
        /// <summary>
        /// Ecrit dans un fichier, si le fichier existe déjà alors il est remplacé sinon un nouveau est créer;
        /// Le chemin du répertoire doit être créé
        /// </summary>
        /// <param name="path">Chemin d'accès du fichier</param>
        /// <param name="name">Nom du fichier</param>
        /// <param name="content">Contenu du fichier</param>
        /// <exception cref="DirectoryNotFoundException">Lever lors ce que le chemin d'accès au fichier n'existe pas</exception>
        /// <exception cref="PathTooLongException">Lever lors ce que le chemin d'accès renseigner est trop long</exception>
        /// <exception cref="UnauthorizedAccessException">Lever lors ce que l'accès au répertoire n'est pas autorisé</exception>
        /// <exception cref="SecurityException">Lever lors ce que l'application n'a pas l'autorisation requise</exception>
        public static void Write (string path, string name, string content)
        {
            if (path[path.Length - 1] == '\\')
                path = path.TrimEnd('\\');

            /*
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException(string.Format("Specified directory not existing: {0}", path));
            }
            */
            string completePath = path + "\\" + name;

            try
            {
                File.WriteAllText(completePath, content);
            }
            catch (PathTooLongException ex)
            {
                throw ex;
            }
            catch (DirectoryNotFoundException ex)
            {
                throw ex;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw ex;
            }
            catch (SecurityException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lit un fichier
        /// </summary>
        /// <param name="path">Chemin d'accès du fichier</param>
        /// <returns>Le contenu du fichier sous forme de chaîne de caractères</returns>
        /// <exception cref="PathTooLongException">Lever lors ce que le chemin d'accès au fichier est trop long</exception>
        /// <exception cref="DirectoryNotFoundException">Lever lors ce que le chemin d'accès du repértoire n'existe pas</exception>
        /// <exception cref="UnauthorizedAccessException">Lever lors ce que le chemin d'accès au fichier n'existe pas</exception>
        /// <exception cref="SecurityException">Lever lors ce que l'application n'a pas l'autorisation d'accèder à ce fichier</exception>
        public static string Read (string path)
        {
            /*
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(string.Format("File not found: {0}", path));
            }
            */

            try
            {
                return File.ReadAllText(path);
            }
            catch(PathTooLongException ex)
            {
                throw ex;
            }
            catch (DirectoryNotFoundException ex)
            {
                throw ex;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw ex;
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }
            catch (SecurityException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
