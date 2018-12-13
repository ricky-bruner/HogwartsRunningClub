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
    }
}
