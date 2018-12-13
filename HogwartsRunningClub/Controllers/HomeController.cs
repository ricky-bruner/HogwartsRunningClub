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
            user.House = _context.House.FirstOrDefault(h => h.HouseId == user.HouseId);
            
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
