using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessageService
{
    public class TestObjectConfig : IEntityTypeConfiguration<TestObject>
    {
        /// <summary>Конфигурация данных из базы.</summary>
        /// <param name="builder"><see cref="EntityTypeBuilder"/><see cref="TestObject"/></param>
        public void Configure(EntityTypeBuilder<TestObject> builder)
        {
            builder.HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .HasColumnName("Id");

            builder
                .Property(m => m.Name)
                .HasColumnName("Name");

            builder
                .Property(m => m.Count)
                .HasColumnName("i");

            builder.ToTable("Table_1", "dbo");
        }
    }
}
