using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    public class SingleAnswerQuestion : AbstractQuestion
    {

        public override int Id { get; set; }

        public override string? Question { get; set; }
        public override List<string>? Answer { get; set; }

        public bool IsCaseSensitive { get; set; }

        public SingleAnswerQuestion(){}

        public SingleAnswerQuestion(int id, string? question, string answer, bool isCaseSensitive) : base(id, question)
        {
            Answer = new List<string>() { answer };
            IsCaseSensitive = isCaseSensitive;
        }

        public SingleAnswerQuestion(string? question, string answer, bool isCaseSensitive) : base(question)
        {
            Id = QuestionReader.GetMaxId(this.GetType());
            Answer = new List<string>() { answer };
            IsCaseSensitive = isCaseSensitive;
        }

        public override bool TestAnswer(string answer)
        {
            string buffer = answer;
            if (!IsCaseSensitive)
            {
                Answer?.Select(x => x.ToLower());
                buffer = buffer.ToLower();
            }
            return Answer.Contains(buffer);
        }
    }
}
