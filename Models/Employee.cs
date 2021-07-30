using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
public class Employee
{
    [Key]
    public long Id { get; set; }
    public string last_name { get; set; }
    public string first_name { get; set; }
    public string title { get; set; }
    public long user_id { get; set; }

}