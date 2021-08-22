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
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string ElevatorType { get; set; }
        public string DateOfCommissioning { get; set; }
        public string DateOfLastInspection { get; set; }
        public string CertificateOfInspection { get; set; }
        public string Info { get; set; }
        public string Notes { get; set; }
        public long column_id { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        public  Column Column { get; }
    }
}    