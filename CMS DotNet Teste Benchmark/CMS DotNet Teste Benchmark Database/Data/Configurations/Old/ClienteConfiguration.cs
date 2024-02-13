//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using TesteBenchmarkDotNet.Models;

//namespace TesteBenchmarkDotNet.Data.Configurations;

//public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
//{
//    public void Configure(EntityTypeBuilder<Cliente> builder)
//    {
//        builder.ToTable("Cliente");

//        builder.HasKey(x => x.Id);

//        builder.Property(e => e.Id).HasColumnName("Id").IsRequired(); // .HasColumnType("char(36)")
//        builder.Property(e => e.Limite).HasColumnName("Limite").IsRequired();
//    }
//}
