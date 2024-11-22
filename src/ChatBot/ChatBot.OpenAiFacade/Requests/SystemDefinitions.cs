namespace ChatBot.OpenAiFacade.Requests;

public static class SystemDefinitions
{
    public const char TagSeparator = ';';
    public const string Default = "You're a helpful assistant which tries his best to answer questions about general knowledge";
    public const string TagsDefinition = "Include tags which will briefly describe the topic of the question, if you're not able to provide tags, provide empty list";
    public const string SummaryDefinition = "Include a summary of the question(summary of intention of the question)";
    public const string ResponseDefinition = "Response message should be in json format which will contain fields: 'answer', 'tags' and 'summary', 'summary' should be shorter than actual question";
}