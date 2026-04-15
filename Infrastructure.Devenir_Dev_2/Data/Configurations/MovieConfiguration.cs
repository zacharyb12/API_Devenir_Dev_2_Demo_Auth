using Domain_Devenir_Dev_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Devenir_Dev_2.Data.Configurations
{
    internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Synopsis)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(m => m.Genre)
                    .IsRequired()
                    .HasMaxLength(20);

            builder.Property(m => m.Director)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(m => m.ReleaseDate)
                    .IsRequired();

            builder.Property(m => m.AverageRating)
                    .IsRequired();





        }
    }
}
