using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    [DataContract]
    public class Question
    {
        [DataMember(Name = "question")]
        public string Text { get; set; }

        [DataMember(Name = "answers")]
        public List<string> Answers { get; set; }
    }
}