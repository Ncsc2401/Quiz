using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    public abstract class AbstractQuestion : IQuestion
    {
        public abstract int Id { get; set; }
        public abstract string? Question { get; set; }
        public abstract List<string>? Answer { get; set; }

        public abstract bool TestAnswer(string answer);

        public AbstractQuestion() { }
        public AbstractQuestion(string? question)
        {
            Question = question;
        }

        public AbstractQuestion(int id, string? question)
        {
            Id = id;
            Question = question;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Question {Id}");
            sb.AppendLine(Question);

            foreach (var ans in Answer)
            {
                sb.AppendLine(ans);
            }
            return sb.ToString();
        }

    }
}
