using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    public class MultipleAnswerQuestion : AbstractQuestion
    {
        public override int Id { get; set; }

        public override string? Question { get; set; }
        public override List<string>? Answer { get; set; }

        public MultipleAnswerQuestion() { }

        public MultipleAnswerQuestion(int id, string? question, params string[] answers) : base(id, question)
        {
            Answer = answers?.ToList();
        }

        public MultipleAnswerQuestion(string? question, params string[] answers) : base(question)
        {
            Id = QuestionReader.GetMaxId(this.GetType());
            Answer = answers.ToList();
        }

        public override bool TestAnswer(string answer)
        {
            return Answer.Contains(answer);
        }
    }
}
