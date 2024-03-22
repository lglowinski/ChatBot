using System.Runtime.Serialization;

namespace ChatBot.Contracts.Requests;

[DataContract]
public class ListQuestionsRequest
{
    [DataMember(Name = "take")]
    public int? Take { get; set; }
    
    [DataMember(Name = "searchTerm")]
    public string? SearchTerm { get; set; }
    
    [DataMember(Name = "orderBy")]
    public string? OrderBy { get; set; }
    [DataMember(Name = "page")]
    public int? Page { get; set; }
}