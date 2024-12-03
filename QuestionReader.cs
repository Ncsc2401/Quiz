using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Quiz
{
    internal static class QuestionReader
    {
        private static readonly string root = @"D:\Users\nicol\Desktop\";
        private static readonly string foldersName = "Questions";
        public static readonly string path = Path.Combine(root, foldersName);

        public static int GetMaxId(Type T)
        {
            try
            {
                List<int> ids = new List<int>();

                foreach (var file in Directory.GetFiles(Path.Combine(path, T.ToString())))
                {
                    string questionId = Path.GetFileNameWithoutExtension(file);
                    ids.Add(Convert.ToInt32(questionId));
                }
                return (ids.Count > 0) ? ids.Max() + 1 : 0;
            }
            catch
            {
                return 0;
            }
        }

        public static List<T>? GetQuestions<T>() where T : IQuestion
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            List<T> result = new List<T>();

            foreach (var file in Directory.GetFiles(Path.Combine(path, typeof(T).ToString())))
            {
                using StreamReader reader = new StreamReader(file);

                result.Add((T)serializer.Deserialize(reader));
            }

            return result;

        }

        public static T? GetQuestion<T>(int id) where T : IQuestion
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            foreach (var file in Directory.GetFiles(Path.Combine(path, typeof(T).ToString())))
            {
                string questionId = Path.GetFileNameWithoutExtension(file);

                if (id == Convert.ToInt32(questionId))
                {
                    using StreamReader reader = new StreamReader(file);

                    return (T)serializer.Deserialize(reader);
                }
            }

            throw new ArgumentException("Question Id not valid.");
        }
    }
}
