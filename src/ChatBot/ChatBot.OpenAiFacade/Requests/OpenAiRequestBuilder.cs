namespace ChatBot.OpenAiFacade.Requests;

public class OpenAiRequestBuilder
{
    private string _model;
    private readonly List<Message> _messages = [];
    
    public OpenAiRequestBuilder WithModel(string model = "gpt-3.5-turbo")
    {
        _model = model;
        return this;
    }
    
    public OpenAiRequestBuilder WithSystemDefinitions(string definition) 
        => WithSystemDefinitions([definition]);
    
    public OpenAiRequestBuilder WithSystemDefinitions(IEnumerable<string> definitions)
    {
        _messages.AddRange(definitions.Select(d => new Message("system", d)));
        return this;
    }
    
    public OpenAiRequestBuilder WithPrompt(string prompt)
    {
        _messages.Add(new Message("user", prompt));
        return this;
    }
    
    public OpenAiRequest Build()
    {
        return new OpenAiRequest(_model, _messages);
    }
}