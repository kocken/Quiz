using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.API.Entities
{
    public class SessionQuiz
    {
        public string SessionId { get; set; }
        public List<Question> Questions { get; set; }

        public SessionQuiz(string sessionId)
        {
            SessionId = sessionId;
            Questions = new List<Question>();
        }
    }
}
