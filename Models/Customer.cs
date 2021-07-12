using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
public class Customer
{
    public long Id { get; set; }
    public string email_of_the_company_contact { get; set; }
    public string technical_manager_email_for_service { get; set; }
    
}