using Microsoft.EntityFrameworkCore;
using PlataformaAPI.Models;
using PlataformaDoentes.Models;
using System.Reflection.Metadata;

namespace PlataformaDoentes.Data
{
    public class PlataformaDoentes_API_DbContext : DbContext
    {
        public PlataformaDoentes_API_DbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Consulta_Paciente> Consultas_Pacientes { get; set; }

        // garantir que IdPaciente e IdConsulta da classe Consulta_Paciente referencie as devidas PKs
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consulta_Paciente>()
                .HasKey(cp => cp.Id);

            modelBuilder.Entity<Consulta_Paciente>()
                .HasOne(cp => cp.Paciente)
                .WithMany(cp => cp.ConsultasPacientes)
                .HasForeignKey(cp => cp.IdPaciente);

            modelBuilder.Entity<Consulta_Paciente>()
                .HasOne(cp => cp.Consulta)
                .WithMany(cp => cp.ConsultasPacientes)
                .HasForeignKey(cp => cp.IdConsulta);
        }
    }
}
