using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace kyrsvoiar.Models
{
    public partial class kyrsarbdContext : DbContext
    {
        public kyrsarbdContext()
        {
        }

        public kyrsarbdContext(DbContextOptions<kyrsarbdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anchor> Anchor { get; set; }
        public virtual DbSet<Building> Building { get; set; }
        public virtual DbSet<Iot> Iot { get; set; }
        public virtual DbSet<Owner> Owner { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=MobileARDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anchor>(entity =>
            {
                entity.HasKey(e => e.Idanchor);

                entity.ToTable("anchor");

                entity.Property(e => e.Idanchor)
                    .HasColumnName("idanchor").ValueGeneratedOnAdd();
                   
                    

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Idiot).HasColumnName("idiot");

            
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.HasKey(e => e.Idbuilding);

                entity.ToTable("building");

                entity.Property(e => e.Idbuilding)
                    .HasColumnName("idbuilding")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Idowner).HasColumnName("idowner");


                entity.Property(e => e.Admincode)
                    .HasColumnName("admincode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                

               
            });

            modelBuilder.Entity<Iot>(entity =>
            {
                entity.HasKey(e => e.Idiot);

                entity.ToTable("iot");

                entity.Property(e => e.Idiot)
                    .HasColumnName("idiot")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Idbuilding).HasColumnName("idbuilding");

                entity.Property(e => e.Coordinatex)
                    .HasColumnName("coordinatex")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Coordinatey)
                    .HasColumnName("coordinatey")
                    .HasMaxLength(50)
                    .IsUnicode(false);



                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasMaxLength(50)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.Idowner);

                entity.ToTable("owner");

                entity.Property(e => e.Idowner)
                    .HasColumnName("idowner")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasColumnName("mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
