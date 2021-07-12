using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
public class Building
{
    [Key]
    public long Id { get; set; }
    public long customer_Id {get; set;}
}