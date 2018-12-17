using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HogwartsRunningClub.Models;
using HogwartsRunningClub.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using HogwartsRunningClub.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HogwartsRunningClub.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // Index action that serves as the Users Profile Page.
        public async Task<IActionResult> Index()
        {

            ApplicationUser user = await GetCurrentUserAsync();

            user.UserTopics = _context.Topic.Where(t => t.UserId == user.Id).ToList();

            if (user.HouseId != null) 
            { 
                user.House = _context.House.FirstOrDefault(h => h.HouseId == user.HouseId);
            }

            user.UserRaces = _context.UserRace
                                .Include(ur => ur.Race)
                                .Where(ur => ur.UserId == user.Id).ToList();
            
            return View(user);
        }


        // Method to load Sorting Page for User
        public async Task<IActionResult> SortingHat() 
        {
            
            ApplicationUser user = await GetCurrentUserAsync();

            if (user.HouseId == null) 
            { 
                List<House> Houses = _context.House.ToList();

                SortingHatViewModel viewmodel = new SortingHatViewModel();

                viewmodel.User = user;
                viewmodel.Houses = Houses;

                return View(viewmodel);
            
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> SortingHat(SortingHatViewModel viewmodel) 
        {


            ApplicationUser user = await GetCurrentUserAsync();

            user.HouseId = viewmodel.SelectedHouseId;

            _context.ApplicationUser.Update(user);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }


        public async Task<IActionResult> ViewGreatHall() 
        {
            ApplicationUser user = await GetCurrentUserAsync();

            GreatHallViewModel viewmodel = new GreatHallViewModel
            {
                User = user,
                NonExclusiveTopics = await _context.Topic
                    .Include(t => t.User)
                    .ThenInclude(u => u.House)
                    .OrderByDescending(t => t.DateCreated)
                    .Where(t => t.HouseExclusive == false)
                    .ToListAsync(),
                Houses = await _context.House.ToListAsync(),
                TopicCategories = await _context.TopicCategory.ToListAsync()
            };

            return View("GreatHall", viewmodel);
        }

        public async Task<IActionResult> ViewCommonRoom() 
        {

            ApplicationUser user = await GetCurrentUserAsync();

            House house = await _context.House.SingleOrDefaultAsync(h => h.HouseId == user.HouseId);

            CommonRoomViewModel viewmodel = new CommonRoomViewModel
            {
                House = house,
                Topics = await _context.Topic.Include(t => t.User).Where(t => t.User.HouseId == house.HouseId).ToListAsync(),
                HouseMembers = await _context.ApplicationUser.Where(u => u.HouseId == house.HouseId).ToListAsync()
            };

            return View("CommonRoom", viewmodel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
