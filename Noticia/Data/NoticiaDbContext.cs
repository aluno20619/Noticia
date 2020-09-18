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

            modelBuilder.Entity<Utilizadores>().HasData(
             new Utilizadores
             { Id = 1, Nome = "Admin", Email = "admin@admin.pt" }
          );
            modelBuilder.Entity<Noticias>().HasData(
              new Noticias
              {
                  Id = 1,
                  Titulo = "Relatório sobre lar de Reguengos enviado à PGR foi feito antes da pandemia",
                  Resumo = "O documento que a ministra da Segurança Social, Ana Mendes Godinho, enviou à Procuradoria-Geral da República - quando o lar de Reguengos contabilizava já 17 óbitos - foi elaborado com base numa visita técnica feita a 11 de março. António Costa disse ter havido um inquérito, mas houve apenas um relatório a dar conta de que o lar cumpria os requisitos legais em matéria de recursos humanos.",
                  Corpo = "O relatório elaborado pela Segurança Social sobre a situação do lar de Reguengos de Monsaraz foi baseado em informações recolhidas pelos fiscais numa visita realizada a 11 de março, ainda antes de ter sido decretada a situação de emergência e dos problemas de contágio que a covid-19 gerou na instituição." + "/n" + "Segundo avança o jornal Expresso,foi esse documento - que é apenas um relatório e não um inquérito,~como afirmou António Costa - que a ministra da Segurança Social, Ana Mendes Godinho,enviou para a Procuradoria - Geral da República(PGR) numa altura em que a Fundação Maria Inácia Perdigão Vogado da Silva, em Reguengos de Monsaraz, contabilizava já 17 óbitos por covid - 19.",
                  Data_De_Publicacao = new DateTime(2020, 2, 2).Date,
                  Visivel = true,
                  UtilizadoresidFK = 1
              }
           );
          //  modelBuilder.Entity<Topicos>().HasData(
          //   new Topicos
          //   { Id = 1, Nome = "Covid19" }
          //);

          //  modelBuilder.Entity<Imagens>().HasData(
          //    new Imagens
          //    { Id = 1, Nome = "image.jpg", Legenda = "Fundação Maria Inácia, em Reguengos de Monsaraz" }
          // );
        }

        public DbSet<Imagens> Imagens { get; set; }
        public DbSet<NI> NI { get; set; }
        public DbSet<Noticias> Noticias { get; set; }
        public DbSet<NT> NT { get; set; }
        public DbSet<Topicos> Topicos { get; set; }
        public DbSet<Utilizadores> Utilizadores { get; set; }
    }
}

