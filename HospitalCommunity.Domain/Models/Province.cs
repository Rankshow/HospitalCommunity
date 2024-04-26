using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalCommunity.Infrastructure.Models;

[Table("provinces")]
public partial class Province
{
    [Key]
    [Column("province_id")]
    [StringLength(2)]
    [Unicode(false)]
    public string ProvinceId { get; set; } = null!;

    [Column("province_name")]
    [StringLength(30)]
    [Unicode(false)]
    public string ProvinceName { get; set; } = null!;

    [InverseProperty("Province")]
    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    [InverseProperty("Province")]
    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
}
