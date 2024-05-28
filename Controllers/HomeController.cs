using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductManagementApp.Models;
using ProductManagementApp.Data;
using Microsoft.AspNetCore.Authorization;


namespace ProductManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            var tanks = _context.Products.ToList();
            return View(tanks);
        }

        public IActionResult Details(int id)
        {
            var tank = _context.Products.FirstOrDefault(p => p.Id == id);
            if (tank == null)
            {
                return NotFound();
            }
            return View(tank);
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
        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return View();
            }

            var tank = _context.Products.FirstOrDefault(p => p.Name.Contains(searchTerm));
            if (tank == null)
            {
                return NotFound(); // or display an error message
            }

            return RedirectToAction("Details", "Home", new { id = tank.Id });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var tank = _context.Products.Find(id);
            if (tank == null)
            {
                return NotFound();
            }

            return DeleteConfirmed(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
    
        public IActionResult DeleteConfirmed(int id)
        {
            var tank = _context.Products.Find(id);
                if (tank == null)
                {
                    return NotFound();
                }

            _context.Products.Remove(tank);
            _context.SaveChanges();

            return RedirectToAction("List", "Home");
        }
        //[Authorize]
        public IActionResult Add()
        {
            return View();
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(Product model, IFormFile ImagePath)
        {
            if (ModelState.IsValid)
            {
                if (ImagePath != null && ImagePath.Length > 0)
                {
                    var fileName = Path.GetFileName(ImagePath.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImagePath.CopyToAsync(stream);
                    }
                    model.ImagePath = "images/" + fileName;
                }
                
                _context.Products.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(model);
        }
        
        public IActionResult EditDescription(int id)
        {
            var tank = _context.Products.Find(id);
            if (tank == null)
            {
                return NotFound();
            }

            var model = new EditDescriptionViewModel
            {
                Id = tank.Id,
                Name = tank.Name,
                Description = tank.Description
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditDescription(EditDescriptionViewModel model)
        {
            
                var tank = _context.Products.Find(model.Id);
                if (tank == null)
                {
                    return NotFound();
                }

                tank.Description = model.Description;
                _context.Update(tank);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = model.Id });
            

        }



        
    }
}
