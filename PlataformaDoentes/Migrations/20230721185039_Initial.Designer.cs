﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlataformaDoentes.Data;

#nullable disable

namespace PlataformaAPI.Migrations
{
    [DbContext(typeof(PlataformaDoentes_API_DbContext))]
    [Migration("20230721185039_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("PlataformaAPI.Models.Consulta_Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdConsulta")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IdConsulta");

                    b.HasIndex("IdPaciente");

                    b.ToTable("Consultas_Pacientes");
                });

            modelBuilder.Entity("PlataformaDoentes.Models.Consulta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataConsulta")
                        .HasColumnType("TEXT");

                    b.Property<int>("Especialidade")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Consultas");
                });

            modelBuilder.Entity("PlataformaDoentes.Models.Paciente", b =>
                {
                    b.Property<int>("Num_Processo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("TEXT");

                    b.Property<int>("Genero")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Num_Processo");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("PlataformaAPI.Models.Consulta_Paciente", b =>
                {
                    b.HasOne("PlataformaDoentes.Models.Consulta", "Consulta")
                        .WithMany("ConsultasPacientes")
                        .HasForeignKey("IdConsulta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlataformaDoentes.Models.Paciente", "Paciente")
                        .WithMany("ConsultasPacientes")
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consulta");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("PlataformaDoentes.Models.Consulta", b =>
                {
                    b.Navigation("ConsultasPacientes");
                });

            modelBuilder.Entity("PlataformaDoentes.Models.Paciente", b =>
                {
                    b.Navigation("ConsultasPacientes");
                });
#pragma warning restore 612, 618
        }
    }
}
