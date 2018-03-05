using System;
using System.Collections.Generic;
using System.Linq;

namespace VPackage.Parser
{
    /// <summary>
    /// Outil de création de message
    /// </summary>
    public static class FrameParser
    {
        #region Fields

        /// <summary>
        /// Caractère utilisé pour séparer les valeurs des noms
        /// </summary>
        private static char nameValueSeparator = '=';

        /// <summary>
        /// Caractère utilisé pour séparer les trames
        /// </summary>
        private static char frameSeparator = ';';

        #endregion

        #region Properties

        /// <summary>
        /// Caractère utilisé pour séparer les valeurs des noms
        /// </summary>
        public static char NameValueSeparator
        {
            get
            {
                return nameValueSeparator;
            }

            set
            {
                nameValueSeparator = value;
            }
        }

        /// <summary>
        /// Caractère utilisé pour séparer les différentes trames
        /// </summary>
        public static char FrameSeparator
        {
            get
            {
                return frameSeparator;
            }

            set
            {
                frameSeparator = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retourne une chaîne de caractères mise en forme avec tous les élèments de la liste
        /// </summary>
        /// <param name="list"></param>
        /// <returns>Chaîne de caractères encodée</returns>
        /// <exception cref="ArgumentNullException">Lever lors ce que le tableau passé en paramètre est nul</exception>
        public static string Encode(List<DataWrapper> list)
        {
            if (list == null)
                throw new ArgumentNullException("La liste passée en paramètre est nul");

            return Encode(list.ToArray());
        }

        /// <summary>
        /// Retourne une chaîne de caractères mise en forme avec tous les éléments du tableau
        /// </summary>
        /// <param name="array">Tableau de données</param>
        /// <returns>Chaîne de caractères encodée</returns>
        /// <exception cref="ArgumentNullException">Lever lors ce que le tableau passé en paramètre est nul</exception>
        public static string Encode(DataWrapper[] array)
        {
            if (array == null)
                throw new ArgumentNullException("Le tableau passé en paramètre est nul");

            string result = "";

            foreach (DataWrapper data in array)
            {
                string parse = Encode(data);
                result += parse + FrameSeparator;
            }

            result = result.TrimEnd(FrameSeparator);

            return result;
        }

        /// <summary>
        /// Retourne une chaîne de caractères mise en forme avec le nom et la valeur passées en paramètre
        /// </summary>
        /// <param name="name">Nom de la variable</param>
        /// <param name="value">Valeur de la variable</param>
        /// <returns>La chaîne de caractères encodée</returns>
        /// <exception cref="ValueContentException">Lever lors ce que la valeur ou le nom renseigné utilise les caractères de séparation</exception>
        /// <exception cref="ArgumentNullException">Lever lors ce qu'un des paramètre est nul</exception>
        public static string Encode(string name, object value)
        {
            if (value == null)
                throw new ArgumentNullException("La valeur passée en paramètre est nul");

            string strValue = value.ToString();

            if (name == null || name == string.Empty)
                throw new ArgumentNullException("La valeur passé pour le nom est nul");
            if (strValue == string.Empty)
                throw new ArgumentNullException("La valeur passé pour la valeur est vide");
               
            if (strValue.Contains(FrameSeparator) || strValue.Contains(NameValueSeparator))
                throw new ValueContentException("La valeur utilisée contient un caractère de séparation");
            if (name.Contains(FrameSeparator) || name.Contains(NameValueSeparator))
                throw new ValueContentException("Le nom utilisé contient un caractère de séparation");

            return string.Format("{0}{1}{2}", name.ToUpper(), NameValueSeparator, strValue);
        }

        /// <summary>
        /// Retourne une chaîne de caractères mise en forme
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>La chaîne de caractères encodée</returns>
        /// <exception cref="ValueContentException">Lever lors ce que la valeur ou le nom renseigné utilise les caractères de séparation</exception>
        /// <exception cref="ArgumentNullException">Lever lors ce qu'un des paramètre est nul</exception>
        public static string Encode(DataWrapper data)
        {
            if (data == null)
                throw new ArgumentNullException("La donnée passé en paramètre est nul");

            return Encode(data.Name, data.Value);
        }

        /// <summary>
        /// Retourne les messages découpés dans une liste d'instance de la structure ParseData
        /// </summary>
        /// <param name="parsedData">Chaîne de caractères encodée</param>
        /// <returns>Liste de données decodée</returns>
        /// <exception cref="WrongFormatException">Lever lors ce qu'il a des erreurs liées aux séparateurs</exception>
        /// <exception cref="ArgumentNullException">Lever lors ce que le paramètre est nul ou vide</exception>
        public static List<DataWrapper> DecodeArray(string parsedData)
        {
            if (parsedData == null || parsedData == string.Empty)
                throw new ArgumentNullException("La chaîne de caractères passée en paramètre est nul ou vide");

            if (!parsedData.Contains(NameValueSeparator) || !parsedData.Contains(FrameSeparator))
                throw new WrongFormatException("La donnée encodée ne contient pas de séparateurs");

            if (parsedData.Count(p => p == NameValueSeparator) != (parsedData.Count(p => p == FrameSeparator) + 1))
                throw new WrongFormatException("Les données ne sont pas encodées correctement");

            string[] exploded = parsedData.Split(FrameSeparator);
            List<DataWrapper> parseDataArray = new List<DataWrapper>();

            foreach (string e in exploded)
            {
                parseDataArray.Add(Decode(e));
            }

            return parseDataArray;
        }

        /// <summary>
        /// Retourne le message découpé dans une instance de la structure ParseData
        /// </summary>
        /// <param name="frame">Message formé</param>
        /// <returns>Donnée decodée</returns>
        /// <exception cref="WrongFormatException">Lever lors ce qu'il n'y a pas de séparateur nom/valeur</exception>
        /// <exception cref="ArgumentNullException">Lever lors ce que l'argument est nul ou vide</exception>
        public static DataWrapper Decode(string frame)
        {
            if (frame == null || frame == string.Empty)
                throw new ArgumentNullException("La chaîne passée en paramètre est nul ou vide");

            if (frame.Contains(NameValueSeparator))
            {
                string[] exploded = frame.Split(NameValueSeparator);
                return new DataWrapper(exploded[0], exploded[1]);
            }
            else
            {
                throw new WrongFormatException("La donnée encodée ne contient pas de séparateur");
            }
        }
            
        /// <summary>
        /// Rassemble deux trames
        /// </summary>
        /// <param name="s0">Première trame</param>
        /// <param name="s1">Seconde trame</param>
        /// <returns>Les deux trames assemblées</returns>
        /// <exception cref="ArgumentNullException">Un des paramètre est nul ou vide</exception>
        public static string Merge (string s0, string s1)
        {
            if (s0 == null || s0 == string.Empty)
                throw new ArgumentNullException("La première chaîne de caractères passée est nul ou vide");
            if (s1 == null || s1 == string.Empty)
                throw new ArgumentNullException("La seconde chaîne de caractères passée est nul ou vide");
                
            return string.Format("{0}{1}{2}", s0, FrameSeparator,s1);
        }

        #endregion

    }
}

