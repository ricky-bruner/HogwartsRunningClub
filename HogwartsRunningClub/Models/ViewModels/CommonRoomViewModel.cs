using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HogwartsRunningClub.Models.ViewModels
{
    public class CommonRoomViewModel
    {
        public House House { get; set; }

        public List<Topic> Topics { get; set; }

        public List<ApplicationUser> HouseMembers { get; set; }

        public List<TopicCategory> TopicCategories { get; set; }

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
