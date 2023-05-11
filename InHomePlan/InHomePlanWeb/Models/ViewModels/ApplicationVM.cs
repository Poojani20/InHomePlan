using InHomePlanWeb.Utility;

namespace InHomePlanWeb.Models.ViewModels
{
    public class ApplicationVM
    {
        public List<Application> ApplicationList { get; set; }
        public List<ApplicationStatusCount> StatusCounts { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }
    }
}
