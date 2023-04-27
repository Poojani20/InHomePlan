using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class ApplicationDetails
    {
       
        public int Id { get; set; }
        [Required]
        public int ApplicationHeaderId { get; set; }
        [ForeignKey("ApplicationHeaderId")]
        [ValidateNever]
        public ApplicationHeader ApplicationHeader { get; set; }

        [Required]
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        [ValidateNever]
        public Application Application { get; set; }

        //public int Count { get; set; }
        //public double Price { get; set; }



    }
}
