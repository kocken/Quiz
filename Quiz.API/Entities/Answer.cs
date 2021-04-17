using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.API.Entities
{
    public class Answer
    {
        public char Letter { get; set; }
        public string Message { get; set; }

        public Answer(char letter, string message)
        {
            Letter = letter;
            Message = message;
        }
    }
}
