using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //return list of all vendors
        public IActionResult GetAllVendor()
        {
            var data = _context.VendorMasters;
            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                return View(data);
            }

        }
        [HttpPost]

        //add new vendor
        public IActionResult AddVendor(VendorMaster vendor)
        {
            
            if(ModelState.IsValid)
            {
                vendor.IsActive = true;
                _context.VendorMasters.Add(vendor);
               
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Vendor added successfully!";

                return RedirectToAction("GetAllVendor"); 
            }
            else
            {
                return BadRequest(ModelState);

            }

        }
        [HttpGet]
       
        public IActionResult AddVendor()
        {
            var lastVendor = _context.VendorMasters.OrderByDescending(v => v.Code).FirstOrDefault();
            string newCode;

           
            if (lastVendor != null)
            {

                int lastNumericPart = int.Parse(lastVendor.Code.Substring(1)); 
                string newNumericPart = (lastNumericPart + 1).ToString("D3"); 
                newCode = "V" + newNumericPart;
            }
            else
            {
               
                newCode = "V001";
            }

            ViewBag.Code = newCode;
            return View();
        }

      

        [HttpGet]
        // update existing vendor
        public IActionResult Edit(int? Id)
        {
            if(Id<0)
            {
                return BadRequest();
            }
           
            VendorMaster data = _context.VendorMasters.FirstOrDefault(a => a.Id==Id);
            if(data==null)
            {
                return NotFound();
            }
            
            return View(data) ;
        }

        [HttpPost]
        public IActionResult Edit(int id, VendorMaster vendorMaster)
        {
            if (id != vendorMaster.Id)
            {
                return BadRequest();
            }

            _context.VendorMasters.Update(vendorMaster);
            _context.SaveChanges();

            // TempData["SuccessMessage"] = "Vendor details updated successfully!";
            return RedirectToAction("GetAllVendor");
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var vendor=_context.VendorMasters.Find(Id);

            return View(vendor);
        }

        [HttpPost]
        //delete existing vendor
        public IActionResult Delete(int? Id)
        {
            if (Id < 0)
            {
                return BadRequest();
            }  

            VendorMaster data = _context.VendorMasters.Find(Id);

            if (data == null)
            {
                return BadRequest();
            }

            string name=data.Code;
            _context.VendorMasters.Remove(data);
            _context.SaveChanges();        

            return RedirectToAction("GetAllVendor");
        }

        [HttpGet]
        public IActionResult userExists(string email, string contactNo)
        {
            var emailExists = _context.VendorMasters.Any(v => v.ContactEmail == email);
            var contactNoExists = _context.VendorMasters.Any(v => v.ContactNo == contactNo);

           
           return Json(new { emailExists, contactNoExists });
        }
    }
}