using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    [DataContract(Name = "question")]
    public class Question
    {
        [DataMember(Name = "question")]
        public string QuestionText { get; set; }

        [DataMember(Name = "answers")]
        public List<string> Answers { get; set; }
    }
}