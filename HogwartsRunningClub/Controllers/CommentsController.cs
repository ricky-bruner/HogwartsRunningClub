using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HogwartsRunningClub.Data;
using HogwartsRunningClub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using HogwartsRunningClub.Models.ViewModels.TopicViewModels;
using HogwartsRunningClub.Models.ViewModels.CommentModels;

namespace HogwartsRunningClub.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> GetComment(int id)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            Comment comment = await _context.Comment.FindAsync(id);

            if (comment.UserId != user.Id) 
            {
                return RedirectToAction("ViewGreatHall", "Home");
            }

            CommentJson commentJson = new CommentJson();
            commentJson.Content = comment.Content;

            return Json(commentJson);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(int TopicId, DetailsTopicViewModel viewmodel) 
        {
            ApplicationUser user = await GetCurrentUserAsync();

            Comment comment = new Comment()
            {
                UserId = user.Id,
                Content = viewmodel.Content,
                TopicId = TopicId
            };

            _context.Add(comment);
            await _context.SaveChangesAsync();


            return RedirectToAction("Details", "Topics", new { id = TopicId });

        }

        //// GET: Comments
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Comment.Include(c => c.Topic).Include(c => c.User);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        //// GET: Comments/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comment = await _context.Comment
        //        .Include(c => c.Topic)
        //        .Include(c => c.User)
        //        .FirstOrDefaultAsync(m => m.CommentId == id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(comment);
        //}

        //// GET: Comments/Create
        //public IActionResult Create()
        //{
        //    ViewData["TopicId"] = new SelectList(_context.Topic, "TopicId", "Content");
        //    ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
        //    return View();
        //}

        //// POST: Comments/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CommentId,Content,DateCreated,UserId,TopicId")] Comment comment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(comment);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["TopicId"] = new SelectList(_context.Topic, "TopicId", "Content", comment.TopicId);
        //    ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", comment.UserId);
        //    return View(comment);
        //}

        //// GET: Comments/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comment = await _context.Comment.FindAsync(id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["TopicId"] = new SelectList(_context.Topic, "TopicId", "Content", comment.TopicId);
        //    ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", comment.UserId);
        //    return View(comment);
        //}

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditComment(int CommentId, Comment Comment)
        {
            Comment comment = await _context.Comment.FindAsync(CommentId);
            comment.Content = Comment.Content;

            _context.Update(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Topics", new { id = comment.TopicId });
        }

        //// GET: Comments/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comment = await _context.Comment
        //        .Include(c => c.Topic)
        //        .Include(c => c.User)
        //        .FirstOrDefaultAsync(m => m.CommentId == id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(comment);
        //}

        // POST: Comments/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            int topicId = comment.TopicId;
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Topics", new { id = topicId });
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.CommentId == id);
        }
    }
}
