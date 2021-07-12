using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
public class Column
{
    [Key]
    public long Id { get; set; }
    public string Status { get; set; }
    public long battery_Id { get; set; }
}