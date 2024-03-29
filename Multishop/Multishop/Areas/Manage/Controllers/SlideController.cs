﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Multishop.Areas.Manage.ViewModels;
using Multishop.Areas.Manage.ViewModels.Slide;
using Multishop.DAL;
using Multishop.Entities;
using Multishop.Utilities.Extensions;

namespace Multishop.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class SlideController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlideController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int count = await _context.Slides.CountAsync();
            List<Slide> slides = await _context.Slides.Skip((page - 1) * 3).Take(3)
                .ToListAsync();
            PaginationVM<Slide> paginationVM = new PaginationVM<Slide>
            {
                Items = slides,
                TotalPage = Math.Ceiling((double)count / 3),
                CurrentPage = page,

            };
            return View(paginationVM);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSlideVM slideVM)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!slideVM.Photo.CheckFile("image/"))
            {
                ModelState.AddModelError("Photo", "Invalid file type");
                return View();

            }
            if (!slideVM.Photo.CheckSize(4 * 1024))
            {
                ModelState.AddModelError("Photo", "File size should not be larger than 4 mb");
                return View();
            }

            string fileName = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "slider");

            Slide slide = new Slide
            {

                Image = fileName,
                Title = slideVM.Title,
                SubTitle = slideVM.SubTitle,
                Description = slideVM.Description,

            };
            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (existed == null) return NotFound();
            UpdateSlideVM slideVM = new UpdateSlideVM
            {
                Title = existed.Title,
                SubTitle = existed.SubTitle,
                Description = existed.Description,
                Image = existed.Image,
            };

            return View(slideVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateSlideVM slideVM)
        {



            if (!ModelState.IsValid)
            {
                return View(slideVM);
            }

            Slide existed = await _context.Slides.FirstOrDefaultAsync(x => x.Id == id);
            if (existed == null) return NotFound();

            if (slideVM.Photo is not null)
            {
                if (!slideVM.Photo.CheckFile("image"))
                {
                    ModelState.AddModelError("Photo", "Invalid file type");
                    return View(slideVM);

                }
                if (!slideVM.Photo.CheckSize(4 * 1024))
                {
                    ModelState.AddModelError("Photo", "File size should not be larger than 4 mb");
                    return View(slideVM);
                }
                string filename = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "slider");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "images", "slider");
            }

            existed.Title = slideVM.Title;
            existed.Description = slideVM.Description;
            existed.SubTitle = slideVM.SubTitle;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (existed == null) return NotFound();
            existed.Image.DeleteFile(_env.WebRootPath, "assets", "images", "slider");

            _context.Slides.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) return BadRequest();

            Slide existed = await _context.Slides.FirstOrDefaultAsync(c => c.Id == id);


            if (existed == null) return NotFound();
            return View(existed);

        }

    }
}
