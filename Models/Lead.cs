using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
public class Lead
{
    public long Id { get; set; }
    public string email { get; set; }
    public DateTime created_at { get; set; }
}