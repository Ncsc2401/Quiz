using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Quiz
{
    internal static class Game
    {
        public static void Start()
        {
            SelectOption();
        }

        private static void SelectOption()
        {
            try
            {
                Console.Clear();
                PrintOptionMenu();
                int option = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                switch (option)
                {
                    case 1:
                        GameRunning();
                        break;
                    case 2:
                        CreateQuestion();
                        break;
                    case 3:
                        DeleteQuestion();
                        break;
                }
            }
            catch
            {
                SelectOption();
            }
        } 

        private static void PrintOptionMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Select an option:");
            sb.AppendLine("1 - Start game");
            sb.AppendLine("2 - Create question");
            sb.AppendLine("3 - Delete question");
            Console.WriteLine(sb.ToString());
        }

        private static void PrintCreateMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Select an option:");
            sb.AppendLine("1 - Single answer question");
            sb.AppendLine("2 - Multiple answer question");
            Console.WriteLine(sb.ToString());
        }

        private static void PrintDeleteMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Select an option:");
            sb.AppendLine("1 - Single answer question");
            sb.AppendLine("2 - Multiple answer question");
            Console.WriteLine(sb.ToString());
        }

        private static void CreateQuestion()
        {
            PrintCreateMenu();
            int option = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (option)
            {
                case 1:
                    Console.WriteLine("Question: ");
                    string? question1 = Console.ReadLine();
                    Console.WriteLine("Answer: ");
                    string answer = Console.ReadLine();
                    Console.WriteLine("Is case sensitive? (Y / N)");
                    bool isCaseSensitive = Console.ReadLine() == "Y";
                    QuestionSaver.SaveQuestion(new SingleAnswerQuestion(question1, answer, isCaseSensitive));
                    break;
                case 2:
                    Console.WriteLine("Question: ");
                    string? question2 = Console.ReadLine();
                    Console.WriteLine("How many answers?: ");
                    int amount = Convert.ToInt32(Console.ReadLine());

                    List<string> answers = new List<string>();
                    for (int i = 0; i < (amount > 0 ? amount : 1); i++){
                        answers.Add(Console.ReadLine());
                    }

                    QuestionSaver.SaveQuestion(new MultipleAnswerQuestion(question2, answers.ToArray()));
                    break;
            }
            SelectOption();
        }

        private static void DeleteQuestion()
        {
            try
            {
                PrintDeleteMenu();
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        foreach (var question in QuestionReader.GetQuestions<SingleAnswerQuestion>())
                        {
                            Console.WriteLine(question.ToString());
                        }
                        Console.Write("Id: ");
                        int id1 = Convert.ToInt32(Console.ReadLine());
                        QuestionSaver.DeleteQuestion<SingleAnswerQuestion>(id1);
                        break;
                    case 2:
                        foreach (var question in QuestionReader.GetQuestions<MultipleAnswerQuestion>())
                        {
                            Console.WriteLine(question.ToString());
                        }
                        Console.Write("Id: ");
                        int id2 = Convert.ToInt32(Console.ReadLine());
                        QuestionSaver.DeleteQuestion<MultipleAnswerQuestion>(id2);
                        break;
                }
                SelectOption();
            }
            catch
            {
                SelectOption();
            }
        }

        private static void GameRunning()
        {
            Random random = new Random();

            List<SingleAnswerQuestion>? singleAnswerQuestions = QuestionReader.GetQuestions<SingleAnswerQuestion>();
            List<MultipleAnswerQuestion>? multipleAnswerQuestions = QuestionReader.GetQuestions<MultipleAnswerQuestion>();

            while (singleAnswerQuestions.Count + multipleAnswerQuestions.Count > 0)
            {
                int questionId = random.Next(1, singleAnswerQuestions.Count + multipleAnswerQuestions.Count);
                if (questionId <= singleAnswerQuestions.Count)
                {
                    SingleAnswerQuestion q = singleAnswerQuestions[questionId - 1];
                    singleAnswerQuestions.Remove(q);
                    Console.WriteLine(q.Question);
                    Console.Write("Answer: ");
                    if (q.TestAnswer(Console.ReadLine()))
                    {
                        Console.WriteLine("Correct!");
                    }
                    else
                    {
                        Console.WriteLine("Wrong!");
                    }
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (questionId > singleAnswerQuestions.Count)
                {
                    MultipleAnswerQuestion q = multipleAnswerQuestions[questionId - singleAnswerQuestions.Count - 1];
                    multipleAnswerQuestions.Remove(q);
                    Console.WriteLine(q.Question);
                    Console.Write("Answer: ");
                    if (q.TestAnswer(Console.ReadLine()))
                    {
                        Console.WriteLine("Correct!");
                    }
                    else
                    {
                        Console.WriteLine("Wrong!");
                    }
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            SelectOption();
        }
    }
}
