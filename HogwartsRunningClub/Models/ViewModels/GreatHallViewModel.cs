using HogwartsRunningClub.Models.ViewModels.PaginationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HogwartsRunningClub.Models.ViewModels
{
    public class GreatHallViewModel
    {

        public ApplicationUser User { get; set; }

        public List<Topic> NonExclusiveTopics { get; set; }

        public List<TopicCategory> TopicCategories { get; set; }

        public List<House> Houses { get; set; }

        public string Category { get; set; }

        public Pager Pager { get; set; }

        public List<string> BtnColors { get; set; } = new List<string>
        {
            "btn-primary",
            "btn-success",
            "btn-danger",
            "btn-warning",
            "btn-info",
            "btn-primary",
            "btn-success",
            "btn-danger",
            "btn-warning",
            "btn-info"
        };
    }
}
