using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FactInterventionApi.Models
{
    public partial class Battery
    {
        [Key]
        public long Id { get; set; }
        public string status { get; set; }
        public long building_id { get; set; }
        public string battery_type { get; set; }
        public string date_of_commissioning { get; set; }
        public string date_of_last_inspection { get; set; }
        public string certificate_of_operations { get; set; }
        public string information { get; set; }
        public string notes { get; set; }
        public string created_at {get; set;}
        [System.Text.Json.Serialization.JsonIgnore]
        public  Building Building { get; set;}
        public virtual ICollection<Column> Columns { get; set;}
    }
}        