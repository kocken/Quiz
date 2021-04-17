using Quiz.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quiz.API.Contracts
{
    public class AnswerContract
    {
        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; }

        [JsonPropertyName("questionId")]
        public int QuestionId { get; set; }

        [JsonPropertyName("answer")]
        public char Answer { get; set; }
    }
}
