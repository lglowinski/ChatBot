using ChatBot.Application.Questions.Commands.DeleteUserQuestionsCommand;
using ChatBot.Common.Communication.Kafka;
using ChatBot.Common.Communication.Requests;
using Confluent.Kafka;
using MediatR;

namespace ChatBot.Api.Workers;

public class KafkaWorker<T>(
    IConsumer<string, UserDeleted> consumer,
    IServiceProvider serviceProvider,
    ILogger<KafkaWorker<T>> logger)
    : BackgroundService where T : IKafkaMessage
{
    private const string TopicName = "userDeleted";
    // private Task _consumeTask;
    
    // protected override Task ExecuteAsync(CancellationToken stoppingToken)
    // {
    //    logger.LogInformation("Starting: {0}", nameof(KafkaWorker<T>));
    //    _consumeTask = ConsumeAsync(stoppingToken);
    //    return Task.CompletedTask;
    // }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        
        consumer.Subscribe(TopicName);

        await using var consumerScope = serviceProvider.CreateAsyncScope();
        var sender = consumerScope.ServiceProvider.GetRequiredService<ISender>();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = consumer.Consume(stoppingToken);
                if (consumeResult is null)
                    continue;

                logger.LogInformation("Received user deleted message for user with email: {0}",
                    consumeResult.Message.Value.UserEmail);
                await sender.Send(new DeleteUserQuestionCommand(consumeResult.Message.Value.UserEmail), stoppingToken);

                consumer.Commit(consumeResult);
            }
            catch (ConsumeException e)
            {
                logger.LogError(e, "Failed to consume message due to: {0}", e.Error.Reason);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}