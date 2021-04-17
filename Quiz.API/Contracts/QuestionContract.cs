using Quiz.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quiz.API.Contracts
{
    public class QuestionContract
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("answers")]
        public IEnumerable<Answer> Answers { get; set; }

        public QuestionContract(int id, string message, IEnumerable<Answer> answers)
        {
            Id = id;
            Message = message;
            Answers = answers;
        }
    }
}
