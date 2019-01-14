using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HogwartsRunningClub.Data;
using HogwartsRunningClub.Models;
using HogwartsRunningClub.Models.ViewModels.TopicViewModels;
using Microsoft.AspNetCore.Identity;

namespace HogwartsRunningClub.Controllers
{
    public class TopicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public TopicsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Topics
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Topic.Include(t => t.TopicCategory).Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Topic topic = await _context.Topic
                .Include(t => t.TopicCategory)
                .Include(t => t.User)
                .ThenInclude(u => u.House)
                .Include(t => t.Comments)
                .ThenInclude(c => c.User)
                .ThenInclude(u => u.House)
                .FirstOrDefaultAsync(m => m.TopicId == id);

            topic.Comments = topic.Comments.OrderByDescending(c => c.DateCreated).ToList();

            ApplicationUser user = await GetCurrentUserAsync();
            House house = await _context.House.SingleOrDefaultAsync(h => h.HouseId == user.HouseId);
            user.House = house;

            if (topic == null)
            {
                return NotFound();
            }

            if (topic.HouseExclusive == true && topic.User.HouseId != user.HouseId) 
            {
                return RedirectToAction("ViewGreatHall", "Home");
            }

            DetailsTopicViewModel viewmodel = new DetailsTopicViewModel();
            viewmodel.Edit = true;

            if (topic.UserId != user.Id) 
            {
                topic.TotalViews++;
                viewmodel.Edit = false;
                
            }

            viewmodel.Topic = topic;
            viewmodel.User = user;
            
            _context.Update(topic);
            await _context.SaveChangesAsync();

            ViewData["scripts"] = new List<string>() {
                "HandleComments"
            };

            return View(viewmodel);
        }

        // GET: Topics/Create
        public async Task<IActionResult> Create(bool? House)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            House house = await _context.House.FirstOrDefaultAsync(h => h.HouseId == user.HouseId);
            user.House = house;
            
            CreateTopicViewModel viewmodel = new CreateTopicViewModel();

            List<TopicCategory> categories = await _context.TopicCategory.ToListAsync();

            List<SelectListItem> categoryOptions = categories.Select(c =>
            {
                 return new SelectListItem
                        {
                            Text = c.Label,
                            Value = c.TopicCategoryId.ToString()
                        };
            }).ToList();

            if (House != false) 
            {
                Topic topic = new Topic();
                topic.HouseExclusive = true;
                viewmodel.Topic = topic;
            }

            viewmodel.CategoryOptions = categoryOptions;
            viewmodel.User = user;

            return View(viewmodel);
        }

        // POST: Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTopicViewModel viewmodel)
        {

            ModelState.Remove("Topic.UserId");
            ModelState.Remove("Topic.User");

            if (ModelState.IsValid)
            {
                viewmodel.Topic.TotalViews = 0;
                viewmodel.Topic.UserId = (await GetCurrentUserAsync()).Id;

                _context.Add(viewmodel.Topic);

                await _context.SaveChangesAsync();

                if (viewmodel.Topic.HouseExclusive == true)
                {
                    return RedirectToAction("ViewCommonRoom", "Home", new { category = "All"});
                }
                else
                { 
                    return RedirectToAction("ViewGreatHall", "Home", new { category = "All" });
                }

            }

            return View(viewmodel);
        }

        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Topic topic = await _context.Topic.FindAsync(id);
            ApplicationUser user = await GetCurrentUserAsync();

            if (topic.UserId != user.Id) 
            {
                return RedirectToAction("Details", new { id = topic.TopicId });
            }

            List<TopicCategory> topicCategories = await _context.TopicCategory.ToListAsync();

            if (topic == null)
            {
                return NotFound();
            }

            EditTopicViewModel viewmodel = new EditTopicViewModel();
            viewmodel.Topic = topic;
            viewmodel.CategoryOptions = topicCategories
                    .Select(tc => new SelectListItem { 
                        Text = tc.Label, 
                        Value = tc.TopicCategoryId.ToString() 
                    })
                    .ToList();


            return View(viewmodel);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTopicViewModel viewmodel)
        {
            if (id != viewmodel.Topic.TopicId)
            {
                return NotFound();
            }

            ModelState.Remove("Topic.User");
            ModelState.Remove("Topic.UserId");

            
            if (ModelState.IsValid)
            {
                try
                {
                    Topic topic = await _context.Topic.SingleOrDefaultAsync(t => t.TopicId == id);
                    topic.Title = viewmodel.Topic.Title;
                    topic.Content = viewmodel.Topic.Content;
                    topic.TopicCategoryId = viewmodel.Topic.TopicCategoryId;
                    topic.HouseExclusive = viewmodel.Topic.HouseExclusive;

                    _context.Update(topic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicExists(viewmodel.Topic.TopicId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = viewmodel.Topic.TopicId });
            }

            List<TopicCategory> topicCategories = await _context.TopicCategory.ToListAsync();
            viewmodel.CategoryOptions = topicCategories
                    .Select(tc => new SelectListItem
                    {
                        Text = tc.Label,
                        Value = tc.TopicCategoryId.ToString()
                    })
                    .ToList();
            return View(viewmodel);
        }

        // GET: Topics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await GetCurrentUserAsync();

            Topic topic = await _context.Topic
                .Include(t => t.TopicCategory)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TopicId == id);

            if (topic == null)
            {
                return NotFound();
            }

            if (topic.UserId != user.Id) 
            {
                return RedirectToAction("ViewGreatHall", "Home");
            }
            
            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            ApplicationUser user = await GetCurrentUserAsync();
            Topic topic = await _context.Topic.SingleOrDefaultAsync(t => t.TopicId == id);

            if (topic.UserId != user.Id) 
            {
                return RedirectToAction("ViewGreatHall", "Home");
            }

            List<Comment> comments = await _context.Comment.Where(c => c.TopicId == topic.TopicId).ToListAsync();

            if (comments.Count > 0) 
            {
                foreach (Comment comment in comments) 
                {
                    _context.Remove(comment);
                }
            }

            _context.Topic.Remove(topic);

            await _context.SaveChangesAsync();
            return RedirectToAction("ViewGreatHall", "Home");
        }

        private bool TopicExists(int id)
        {
            return _context.Topic.Any(e => e.TopicId == id);
        }
    }
}
