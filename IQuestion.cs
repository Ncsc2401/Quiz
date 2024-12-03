using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    internal interface IQuestion
    {
        public int Id { get; set; }

        public string? Question { get; set; }
        public List<string>? Answer { get; set; }

        public bool TestAnswer(string answer);
    }
}
