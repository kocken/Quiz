using Quiz.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.API
{
    public class QuestionList
    {
        public Question[] Questions { get; set; }

        public QuestionList(Question[] questions)
        {
            Questions = questions;
        }
    }
}
