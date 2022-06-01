﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MedicalSystem.Models;

namespace MedicalSystem.Data
{
    public partial class MedicalSystemContext : DbContext
    {
        public MedicalSystemContext()
        {
        }

        public MedicalSystemContext(DbContextOptions<MedicalSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clinic> Clinics { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Record> Records { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<Works_in> Works_ins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=medicalsystemserver.database.windows.net;Initial Catalog=MedicalSystem;Persist Security Info=True;User ID=rowan;Password=Password123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>(entity =>
            {
                entity.HasKey(e => new { e.DID, e.PID, e.file_description });

                entity.HasOne(d => d.DIDNavigation)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.DID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Record_Doctor");

                entity.HasOne(d => d.PIDNavigation)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.PID)
                    .HasConstraintName("FK_Record_Patient");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.HasKey(e => new { e.PID, e.DID, e.CID });

                entity.HasOne(d => d.CIDNavigation)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.CID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_Clinic");

                entity.HasOne(d => d.DIDNavigation)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.DID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_Doctor");

                entity.HasOne(d => d.PIDNavigation)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.PID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_Patient");
            });

            modelBuilder.Entity<Works_in>(entity =>
            {
                entity.HasKey(e => new { e.DID, e.CID });

                entity.HasOne(d => d.CIDNavigation)
                    .WithMany(p => p.Works_ins)
                    .HasForeignKey(d => d.CID)
                    .HasConstraintName("FK_Works_in_Clinic");

                entity.HasOne(d => d.DIDNavigation)
                    .WithMany(p => p.Works_ins)
                    .HasForeignKey(d => d.DID)
                    .HasConstraintName("FK_Works_in_Doctor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}