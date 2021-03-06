using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
public class Intervention
{
    [Key]
    public long id { get; set; }
    public long author_id { get; set; }
    public long customer_id { get; set; }
    public long building_id { get; set; }
    public long? battery_id { get; set; }
    public long? column_id { get; set; }
    public long? elevator_id { get; set; }
    public long? employee_id { get; set; }
    public DateTime? start_date { get; set; }
    public DateTime? end_date { get; set; }
    public string result { get; set; }
    public string report { get; set; }
    public string status { get; set; }
    
}