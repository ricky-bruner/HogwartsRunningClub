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
using HogwartsRunningClub.Models.ViewModels.PaginationModels;

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

            user.UserTopics = _context.Topic
                .Include(t => t.Comments)
                .Where(t => t.UserId == user.Id)
                .ToList();

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


        public async Task<IActionResult> ViewGreatHall(int? page, string category) 
        {
            ApplicationUser user = await GetCurrentUserAsync();
            
            List<House> houses = await _context.House.ToListAsync();

            List<TopicCategory> categories = await _context.TopicCategory.ToListAsync();
            
            List<Topic> topics = new List<Topic>();

            if (category == "All")
            {
                topics = await _context.Topic
                        .Include(t => t.Comments)
                        .ThenInclude(c => c.User)
                        .Include(t => t.User)
                        .ThenInclude(u => u.House)
                        .OrderByDescending(t => t.DateCreated)
                        .Where(t => t.HouseExclusive == false)
                        .ToListAsync();
            }
            else 
            {
                TopicCategory tc = categories.Where(cat => cat.Label == category).SingleOrDefault();
                topics = await _context.Topic
                    .Include(t => t.Comments)
                    .ThenInclude(c => c.User)
                    .Include(t => t.User)
                    .ThenInclude(u => u.House)
                    .OrderByDescending(t => t.DateCreated)
                    .Where(t => t.HouseExclusive == false && t.TopicCategoryId == tc.TopicCategoryId)
                    .ToListAsync();
            }



            Pager pager = new Pager(topics.Count(), page);

            GreatHallViewModel viewmodel = new GreatHallViewModel
            {
                User = user,
                NonExclusiveTopics = topics
                    .Skip((pager.CurrentPage - 1) * pager.PageSize)
                    .Take(pager.PageSize)
                    .ToList(),
                Houses = houses,
                TopicCategories = categories,
                Pager = pager,
                Category = category
            };


            return View("GreatHall", viewmodel);
        }

        public async Task<IActionResult> ViewCommonRoom(int? page, string category) 
        {

            ApplicationUser user = await GetCurrentUserAsync();

            House house = await _context.House.SingleOrDefaultAsync(h => h.HouseId == user.HouseId);

            List<TopicCategory> categories = await _context.TopicCategory.ToListAsync();

            List<Topic> topics = new List<Topic>();

            if (category == "All")
            {
                topics = await _context.Topic
                        .Include(t => t.Comments)
                        .ThenInclude(c => c.User)
                        .Include(t => t.User)
                        .ThenInclude(u => u.House)
                        .OrderByDescending(t => t.DateCreated)
                        .Where(t => t.HouseExclusive == true && t.User.HouseId == house.HouseId)
                        .ToListAsync();
            }
            else
            {
                TopicCategory tc = categories
                    .Where(cat => cat.Label == category)
                    .SingleOrDefault();

                topics = await _context.Topic
                    .Include(t => t.Comments)
                    .ThenInclude(c => c.User)
                    .Include(t => t.User)
                    .ThenInclude(u => u.House)
                    .OrderByDescending(t => t.DateCreated)
                    .Where(t => t.HouseExclusive == true && t.TopicCategoryId == tc.TopicCategoryId && t.User.HouseId == house.HouseId)
                    .ToListAsync();
            }

            Pager pager = new Pager(topics.Count(), page);

            CommonRoomViewModel viewmodel = new CommonRoomViewModel
            {
                House = house,
                HouseTopics = topics
                    .Skip((pager.CurrentPage - 1) * pager.PageSize)
                    .Take(pager.PageSize)
                    .ToList(),
                HouseMembers = await _context.ApplicationUser
                    .Where(u => u.HouseId == house.HouseId)
                    .ToListAsync(),
                TopicCategories = categories,
                Pager = pager,
                Category = category,
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
