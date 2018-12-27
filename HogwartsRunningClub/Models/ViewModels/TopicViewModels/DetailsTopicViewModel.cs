using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HogwartsRunningClub.Models.ViewModels.TopicViewModels
{
    public class DetailsTopicViewModel
    {
        public Topic Topic { get; set; }

        public ApplicationUser User { get; set; }

        public bool Edit { get; set; }

        public string Content { get; set; }

    }
}
