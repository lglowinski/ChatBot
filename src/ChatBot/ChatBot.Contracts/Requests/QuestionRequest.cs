using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ChatBot.Contracts.Requests;

[DataContract]
public class QuestionRequest
{
    [DataMember(Name = "question")]
    [MaxLength(256)]
    [Required]
    public required string Question { get; set; } 
}