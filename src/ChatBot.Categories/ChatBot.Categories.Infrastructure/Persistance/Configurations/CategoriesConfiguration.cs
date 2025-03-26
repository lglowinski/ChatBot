using ChatBot.Categories.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatBot.Categories.Infrastructure.Persistance.Configurations;

public class CategoriesConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}