﻿using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class Status
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }  
        public string StatusDescription { get; set; }  
        public DateTime Status_date { get; set; }

        [ForeignKey("Application")]
        public int ApplicationID { get; set; }

        public virtual Application Application { get; set; }
    }
}
