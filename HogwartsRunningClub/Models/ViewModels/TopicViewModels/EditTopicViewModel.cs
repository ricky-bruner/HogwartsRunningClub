using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HogwartsRunningClub.Models.ViewModels.TopicViewModels
{
    public class EditTopicViewModel
    {

        public Topic Topic { get; set; }

        public List<SelectListItem> CategoryOptions { get; set; }
    }
}
