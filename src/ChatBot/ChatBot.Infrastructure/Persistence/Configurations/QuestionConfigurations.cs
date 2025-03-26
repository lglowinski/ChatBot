using ChatBot.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatBot.Infrastructure.Persistence.Configurations;

public class QuestionConfigurations : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(q => q.Id);

        builder.HasIndex(q => q.CreatedAt);

        builder.HasIndex(q => q.HelpfulCount);
        builder.HasIndex(q => q.AuthorEmail);
        builder.HasQueryFilter(q => !q.IsDeleted);
    }
}