using ChatBot.Contracts.Requests;

namespace ChatBot.Api.Bindings;

internal static class ListQuestionsRequestBinding
{
    internal static ValueTask<ListQuestionsRequest> BindAsync(HttpContext context)
        => ValueTask.FromResult(new ListQuestionsRequest
        {
            OrderBy = !string.IsNullOrEmpty(context.Request.Query["orderBy"])
                ? context.Request.Query["orderBy"].ToString()
                : null,
            SearchTerm = !string.IsNullOrEmpty(context.Request.Query["searchTerm"])
                ? context.Request.Query["searchTerm"].ToString()
                : null,
            Take = int.TryParse(context.Request.Query["take"], out var take) ? take : null
        });
}