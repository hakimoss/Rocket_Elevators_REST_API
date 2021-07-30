using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FactInterventionApi.Models
{
    public class Building
    {
        [Key]
        public long Id { get; set; }
        public long? customer_Id {get; set;}
        public string full_name_of_the_building_administrator {get; set;}
        public string email_of_the_administrator {get; set;}
        public string phone_number_of_the_building_administrator {get; set;}
        public string full_name_of_the_technical_contact_for_the_building {get; set;}
        public string technical_contact_email_for_the_building {get; set;}
        public string technical_contact_phone_for_the_building {get; set;}
        public long? address_of_the_building_id {get; set;}
        public string created_at {get; set;}
        public virtual ICollection<Battery> Batteries { get; set;}
        [System.Text.Json.Serialization.JsonIgnore]
        public  Customer Customer { get; set; }
    }
}
