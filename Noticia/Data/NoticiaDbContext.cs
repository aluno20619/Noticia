using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Noticia.Models;

namespace Noticia.Data
{
    public class NoticiaDbContext : IdentityDbContext
    {
        public NoticiaDbContext(DbContextOptions<NoticiaDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<IdentityRole<string>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

            modelBuilder.Entity<IdentityUserLogin<string>>()
               .HasKey(x => new { x.ProviderKey, x.LoginProvider });

            modelBuilder.Entity<IdentityUserRole<string>>()
               .HasKey(x => new { x.UserId, x.RoleId });


            modelBuilder.Entity<NI>()
              .HasKey(x => new { x.Imagensid, x.Noticiasid });



            modelBuilder.Entity<NI>()
               .HasOne<Imagens>(x => x.Imagens)
               .WithMany(c => c.ListaNI)
               .HasForeignKey(x => x.Imagensid);
            modelBuilder.Entity<NI>()
                .HasOne<Noticias>(x => x.Noticias)
                .WithMany(c => c.ListaNI)
                .HasForeignKey(x => x.Noticiasid);


            modelBuilder.Entity<NT>()
                .HasKey(o => new { o.Topicosid, o.Noticiasid });


            modelBuilder.Entity<NT>()
               .HasOne<Topicos>(x => x.Topicos)
               .WithMany(c => c.ListaNT)
               .HasForeignKey(x => x.Topicosid);
            modelBuilder.Entity<NT>()
                .HasOne<Noticias>(x => x.Noticias)
                .WithMany(c => c.ListaNT)
                .HasForeignKey(x => x.Noticiasid);

        }

        public DbSet<Imagens> Imagens { get; set; }
        public DbSet<NI> NI { get; set; }
        public DbSet<Noticias> Noticias { get; set; }
        public DbSet<NT> NT { get; set; }
        public DbSet<Topicos> Topicos { get; set; }
        public DbSet<Utilizadores> Utilizadores { get; set; }
    }
}

