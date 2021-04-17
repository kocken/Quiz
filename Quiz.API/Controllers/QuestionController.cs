using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quiz.API.Contracts;
using Quiz.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.API.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionController : ControllerBase
    {
        private static List<SessionQuiz> _sessions = new List<SessionQuiz>();

        private readonly QuestionList _questionList;

        public QuestionController(QuestionList questionList)
        {
            _questionList = questionList;
        }

        [HttpGet]
        [Route("next")]
        public async Task<ActionResult<QuestionContract>> NextQuestion(string sessionId)
        {
            if (_sessions.Any(x => x.SessionId == sessionId)) // existing session
            {
                var session = _sessions.First(x => x.SessionId == sessionId);

                var remainingQuestions = _questionList.Questions.Where(q => !session.Questions.Any(sq => sq.Id == q.Id)).ToArray();

                if (remainingQuestions.Length == 0)
                    return NotFound();

                var question = remainingQuestions[new Random().Next(remainingQuestions.Count())];

                session.Questions.Add(question);

                var contract = new QuestionContract(question.Id, question.Message, question.Answers);

                return contract;
            }
            else // new session
            {
                var question = _questionList.Questions[new Random().Next(_questionList.Questions.Length)];

                var session = new SessionQuiz(sessionId);
                session.Questions.Add(question);

                _sessions.Add(session);

                var contract = new QuestionContract(question.Id, question.Message, question.Answers);

                return contract;
            }
        }

        [HttpPost]
        [Route("answer")]
        public async Task<ActionResult> AnswerQuestion(AnswerContract contract)
        {
            var session = _sessions.FirstOrDefault(x => x.SessionId == contract.SessionId);

            if (session == null)
                return NotFound();

            var question = session.Questions.SingleOrDefault(x => x.Id == contract.QuestionId);

            if (question == null)
                return NotFound();

            question.Answer = contract.Answer;

            return Ok();
        }

        [HttpGet]
        [Route("points")]
        public async Task<ActionResult<int>> GetPoints(string sessionId)
        {
            var session = _sessions.FirstOrDefault(x => x.SessionId == sessionId);

            if (session == null)
                return NotFound();

            var correctAnswers = session.Questions.Count(x => x.Answer == x.CorrectAnswer);

            return correctAnswers;
        }
    }
}
