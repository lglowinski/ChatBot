using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ChatBot.Contracts.Requests;

[DataContract]
public class CreateQuestionRequest
{
    [DataMember(Name = "question")]
    [MaxLength(256)]
    [Required]
    public required string Question { get; set; } 
    [DataMember(Name = "authorEmail")]
    [EmailAddress]
    [Required]
    public required string AuthorEmail { get; set; }
}