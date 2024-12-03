using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Quiz
{
    internal static class QuestionSaver
    {

        private static readonly string root = @"D:\Users\nicol\Desktop\";
        private static readonly string foldersName = "Questions";
        private static readonly string path = Path.Combine(root, foldersName);

        private static void CreateFolder(string QuestionType)
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(path, QuestionType));
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(path);
                CreateFolder(QuestionType);
            }
            catch 
            {
                Console.WriteLine("Unable to create the save folder");
                throw;
            }
        }

        public static void SaveQuestion(IQuestion question)
        {
            try
            {
                XmlSerializer questionSerializer = new XmlSerializer(question.GetType());

                string fileName = $"{question.Id}.xml";

                using StreamWriter sw = new StreamWriter(Path.Combine(path, question.GetType().ToString(), fileName));

                questionSerializer.Serialize(sw, question);
            }
            catch (DirectoryNotFoundException)
            {
                CreateFolder(question.GetType().ToString());
                SaveQuestion(question);
            }
            catch
            {
                throw;
            }
        }

        public static bool DeleteQuestion<T>(int id)
        {
            foreach(var file in Directory.GetFiles(Path.Combine(path, typeof(T).ToString())))
            {
                if (Convert.ToInt32(Path.GetFileNameWithoutExtension(file).Split('-')[1]) == id)
                {
                    File.Delete(file);
                    return true;
                }
            }
            throw new ArgumentException("Invalid question Id.");
        }


    }
}
