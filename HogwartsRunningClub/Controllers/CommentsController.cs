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

        
        //Get A single comment, primary task is to return JSON to a user who is cancelling an Edit.
        public async Task<IActionResult> GetComment(int id)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            Comment comment = await _context.Comment.FindAsync(id);

            if (comment.UserId != user.Id) 
            {
                return RedirectToAction("ViewGreatHall", "Home", new { category = "All" });
            }

            CommentJson commentJson = new CommentJson();
            commentJson.Content = comment.Content;

            return Json(commentJson);
        }


        //Post Create a comment, no GET due to being dynamically rendered via JavaScript
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(int TopicId, Comment Comment) 
        {
            ApplicationUser user = await GetCurrentUserAsync();

            Comment.UserId = user.Id;

            _context.Add(Comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Topics", new { id = TopicId });

        }


        // POST: Comments/Edit/5 -- No GET due to JavaScript dynamic rendering
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditComment(int CommentId, Comment Comment)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            
            Comment comment = await _context.Comment.FindAsync(CommentId);

            if (comment.UserId != user.Id)
            {
                return RedirectToAction("ViewGreatHall", "Home", new { category = "All" });
            }
            else 
            { 
                comment.Content = Comment.Content;

                _context.Update(comment);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Topics", new { id = comment.TopicId });
            
            }
        }


        // POST: Comments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int id)
        {
            ApplicationUser user = await GetCurrentUserAsync();
        
            Comment comment = await _context.Comment.FindAsync(id);

            if (comment.UserId != user.Id) 
            {
                return RedirectToAction("ViewGreatHall", "Home", new { category = "All" });
            } 
            else 
            { 
                int topicId = comment.TopicId;

                _context.Comment.Remove(comment);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Topics", new { id = topicId });
            }

        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.CommentId == id);
        }
    }
}
