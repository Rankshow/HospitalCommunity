using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalCommunity.Infrastructure.Models;

[Table("purchase_order_lines")]
public partial class PurchaseOrderLine
{
    [Key]
    [Column("purchase_order_line_id")]
    public int PurchaseOrderLineId { get; set; }

    [Column("purchase_order_id")]
    public int? PurchaseOrderId { get; set; }

    [Column("line_num")]
    public int? LineNum { get; set; }

    [Column("item_id")]
    public int? ItemId { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }

    [Column("unit_cost", TypeName = "decimal(9, 2)")]
    public decimal? UnitCost { get; set; }

    [Column("received")]
    public int? Received { get; set; }

    [Column("cancelled")]
    public int? Cancelled { get; set; }

    [Column("last_arrived_date", TypeName = "date")]
    public DateTime? LastArrivedDate { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("PurchaseOrderLines")]
    public virtual Item? Item { get; set; }

    [ForeignKey("PurchaseOrderId")]
    [InverseProperty("PurchaseOrderLines")]
    public virtual PurchaseOrder? PurchaseOrder { get; set; }
}
