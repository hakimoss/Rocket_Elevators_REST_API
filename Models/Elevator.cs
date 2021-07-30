using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace FactInterventionApi.Models
{
    public class Elevator
    {
        [Key]
        public long Id { get; set; }
        public string Status { get; set; }
        public string serial_number { get; set; }
        public string model { get; set; }
        public string elevator_type { get; set; }
        public string date_of_commissioning { get; set; }
        public string date_of_last_inspection { get; set; }
        public string certificate_of_inspection { get; set; }
        public string information { get; set; }
        public string notes { get; set; }
        public long column_id { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        public  Column Column { get; }
    }
}    