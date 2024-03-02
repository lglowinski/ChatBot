using MediatR;

namespace ChatBot.Application.Questions.AskQuestion;

public class QuestionCommand(string question) : IRequest
{
}