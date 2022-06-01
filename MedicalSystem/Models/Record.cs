﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Models
{
    [Table("Record")]
    public partial class Record
    {
        [Key]
        public int DID { get; set; }
        [Key]
        public int PID { get; set; }
        [Key]
        [StringLength(150)]
        [Unicode(false)]
        public string file_description { get; set; }
        public byte[] attached_files { get; set; }
        public DateTime? date { get; set; }
        [Unicode(false)]
        public string summary { get; set; }

        [ForeignKey("DID")]
        [InverseProperty("Records")]
        public virtual Doctor DIDNavigation { get; set; }
        [ForeignKey("PID")]
        [InverseProperty("Records")]
        public virtual Patient PIDNavigation { get; set; }
    }
}