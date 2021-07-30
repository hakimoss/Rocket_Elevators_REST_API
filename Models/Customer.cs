using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FactInterventionApi.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public string email_of_the_company_contact { get; set; }
        public string technical_manager_email_for_service { get; set; }
        public string compagny_name { get; set; }
        public string full_name_of_the_company_contact { get; set; }
        public string company_contact_phone { get; set; }
        public string full_name_of_service_technical_authority { get; set; }
        public string technical_authority_phone_for_service { get; set; }
        public string company_description { get; set; }
        public long user_id { get; set; }
        public long company_headquarters_address_id { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
        
    }
}    