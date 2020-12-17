using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyMusic.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusic.Data.Configuration
{
    class MusicConfiguration : IEntityTypeConfiguration<Music>
    {
        public void Configure(EntityTypeBuilder<Music> builder)
        {

            builder
                .HasKey(a => a.ID);
            builder
                .Property(m => m.ID)
                .UseIdentityColumn();
            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .HasOne(m => m.Artist)
                .WithMany(m=>m.Musics)
                .HasForeignKey(m=>m.ArtistId);
            builder
                .ToTable("Musics");

        }
    }
}
