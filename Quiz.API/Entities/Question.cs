using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.API.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public char CorrectAnswer { get; set; }
        public char Answer { get; set; }

        public Question(int id, string message, IEnumerable<Answer> answers, char correctAnswer)
        {
            Id = id;
            Message = message;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }
    }
}
