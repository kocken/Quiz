using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quiz.API.Contracts;
using Quiz.API.Controllers;
using Quiz.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Quiz.API.Tests
{
    public class QuestionControllerTests
    {
        [Fact]
        public async Task GetPoints_Returns_The_Correct_Number()
        {
            // Arrange
            var count = 2;
            var questionList = GetQuestionList();
            var controller = new QuestionController(questionList);
            var sessionId = "session1337";

            // Act
            for (int i = 0; i < questionList.Questions.Count(); i++)
            {
                var questionActionResult = await controller.NextQuestion(sessionId);
                await controller.AnswerQuestion(new AnswerContract { SessionId = sessionId, QuestionId = questionActionResult.Value.Id, Answer = 'A' });
            }
            var pointsActionResult = await controller.GetPoints(sessionId);

            // Assert
            Assert.Equal(count, pointsActionResult.Value);
        }

        public QuestionList GetQuestionList()
        {
            var questionList = new QuestionList(new Question[] { 
                new Question(0, "What is the height of the Eifel tower?", new[]{ new Answer('A', "324"), new Answer('B', "277"), new Answer('C', "365") }, 'A'),
                new Question(1, "How many bits are in 8 kilobytes?", new[]{ new Answer('A', "64000"), new Answer('B', "8000"), new Answer('C', "65536") }, 'C'),
                new Question(2, "What is the speed of light in vaccume?", new[]{ new Answer('A', "25600 miles per second"), new Answer('B', "186000 miles per second"), new Answer('C', "79100000 miles per hour") }, 'B'),
                new Question(3, "Which year was Nikola Tesla born?", new[]{ new Answer('A', "1889"), new Answer('B', "1832"), new Answer('C', "1856") }, 'C'),
                new Question(4, "Which year did the JavaScript programming language appear for the first time?", new[]{ new Answer('A', "1995"), new Answer('B', "2000"), new Answer('C', "1998") }, 'A')
            });

            return questionList;
        }
    }
}
