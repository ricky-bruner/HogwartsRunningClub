using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HogwartsRunningClub.Models.ViewModels
{
    public class CommonRoomViewModel : GreatHallViewModel
    {
        public House House { get; set; }

        public List<Topic> HouseTopics { get; set; }

        public List<ApplicationUser> HouseMembers { get; set; }

    }
}
