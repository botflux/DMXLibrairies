using System.IO;

namespace VPackage.Files
{
    /// <summary>
    /// Gestionnaire de fichiers
    /// </summary>
    public static class FileManager
    {
        /// <summary>
        /// Nombre de caractères maximum d'un nom de fichier
        /// </summary>
        public const int PATH_MAX_SIZE = 259;

        /// <summary>
        /// Options d'écriture de fichier
        /// </summary>
        public enum WriteOptions
        {
            NotCreateDirectory = 0,
            CreateDirectory = 1
        };

        /// <summary>
        /// Ecrit dans un fichier, si le fichier existe déjà alors il est remplacé sinon un nouveau est créer;
        /// Le chemin du répertoire doit être créé
        /// </summary>
        /// <param name="path">Chemin d'accès du fichier</param>
        /// <param name="content">Contenu du fichier</param>
        /// <param name="options">Options d'écriture</param>
        /// <exception cref="DirectoryNotFoundException">Lever lors ce que le chemin d'accès au fichier n'existe pas</exception>
        /// <exception cref="PathTooLongException">Lever lors ce que le chemin d'accès renseigner est trop long</exception>
        public static void Write (string path, string content, WriteOptions options = WriteOptions.NotCreateDirectory)
        {

            if (path.Length >= PATH_MAX_SIZE)
                throw new PathTooLongException("Le chemin d'accès du fichier est trop long");
            
            string directoryPath = Path.GetDirectoryName(path);

            if (!Directory.Exists(directoryPath))
                if (options == WriteOptions.NotCreateDirectory)
                    throw new DirectoryNotFoundException("Le chemin d'accès au fichier n'existe pas");
                else
                    Directory.CreateDirectory(directoryPath);

            File.WriteAllText(path, content);
            
        }

        /// <summary>
        /// Lit un fichier
        /// </summary>
        /// <param name="path">Chemin d'accès du fichier</param>
        /// <returns>Le contenu du fichier sous forme de chaîne de caractères</returns>
        /// <exception cref="PathTooLongException">Lever lors ce que le chemin d'accès au fichier est trop long</exception>
        /// <exception cref="DirectoryNotFoundException">Lever lors ce que le chemin d'accès du repértoire n'existe pas</exception>
        /// <exception cref="FileNotFoundException">Lever lors ce que le fichier n'est pas trouvé</exception>
        public static string Read (string path)
        {
            if (path.Length >= PATH_MAX_SIZE)
                throw new PathTooLongException("Le chemin spécifié est trop long");
            
            string directoryPath = Path.GetDirectoryName(path);
            
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException("Le chemin du repértoire spécifié n'existe pas");
            if (!File.Exists(path))
                throw new FileNotFoundException("Le chemin du fichier spécifié n'existe pas");

            return File.ReadAllText(path);

        }
    }
}
