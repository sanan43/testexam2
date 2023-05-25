using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bilet2.DAL;
using bilet2.Models;
using bilet2.Areas.Admin.ViewModels;
using Microsoft.Extensions.Hosting;
using bilet2.Utilities.Constants;
using bilet2.Utilities.Extensions;

namespace bilet2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamsController : Controller
    {
        private readonly AppDbContext _context;
        public readonly IWebHostEnvironment _environment;

        public TeamsController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        public async Task<IActionResult> Index()
        {
            ICollection<Team> team = await _context.Teams.ToListAsync();
            return View(team);
        }

       
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVM teamVM)
        {
            if (!ModelState.IsValid)
            {
                return View(teamVM);

            }
            if (!teamVM.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", ErrorMessages.FileMustBeImageType);
                return View(teamVM);
            }
            if (!teamVM.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", ErrorMessages.FileSizeMustLessThan200KB);
                return View(teamVM);
            }
            string rootPath = Path.Combine(_environment.WebRootPath, "assets", "img","team");
            string fileName = await teamVM.Photo.SaveAsync(rootPath);
            Team team = new Team()
            {
                Title = teamVM.Title,
                Description = teamVM.Description,
                Imagepath = fileName,
            };
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
          
        }


        public async Task<IActionResult> Update(int? id)
        {
            Team team = await _context.Teams.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            UpdateVM update = new UpdateVM()
            {
                Title = team.Title,
                Description = team.Description,
                Id = id.Value,

            };
            return View(update);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateVM updateVM)
        {
            if (!ModelState.IsValid)
            {
                return View(updateVM);

            }
            if (!updateVM.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", ErrorMessages.FileMustBeImageType);
                return View(updateVM);
            }
            if (!updateVM.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", ErrorMessages.FileSizeMustLessThan200KB);
                return View(updateVM);
            }
            string rootPath = Path.Combine(_environment.WebRootPath, "assets", "img", "team");
            Team team = await _context.Teams.FindAsync(updateVM.Id);

            string filePath = Path.Combine(rootPath, team.Imagepath);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            string newFileName = await updateVM.Photo.SaveAsync(rootPath);

            team.Imagepath = newFileName;
            team.Title = updateVM.Title;
            team.Description = updateVM.Description;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            Team? team = _context.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }
            _context.Teams.Remove(team);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
