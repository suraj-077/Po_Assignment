using Microsoft.AspNetCore.Mvc;
using Po_Assignment.Dal;
using Po_Assignment.Models;

namespace Po_Assignment.Controllers
{
    public class VendorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorController(ApplicationDbContext _context)
        {
            this._context = _context;

        }
        [HttpGet]
        public IActionResult GetAllVendor()
        {
            var data = _context.VendorMasters;
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return View(data);
            }

        }
        [HttpPost]
        public ActionResult AddVendor(VendorMaster vendor)
        {
            if(ModelState.IsValid)
            {
                _context.VendorMasters.Add(vendor);
                _context.SaveChanges();
                return RedirectToAction("GetAllVendor");
            }
            else
            {
               
                return View("Error"); 
            }
          
        }
        [HttpGet]
        public IActionResult AddVendor()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            VendorMaster data = _context.VendorMasters.FirstOrDefault(a => a.Id==Id);
            return View(data) ;
        } 

        [HttpPost]
        public IActionResult Delete(int? Id)
        {
            VendorMaster data = _context.VendorMasters.FirstOrDefault(a => a.Id == Id);

            return View();
        }
    }
}