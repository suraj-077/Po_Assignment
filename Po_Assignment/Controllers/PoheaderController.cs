using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Po_Assignment.Dal;
using Po_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Po_Assignment.Controllers
{
    public class PoHeaderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PoHeaderController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Index()
        {
            return View(_context.PoHeadersMaster.ToList());
        }

        private List<VendorMaster> GetVendors()
        {
            return _context.VendorMasters.ToList();
        }

        private List<MaterialEntry> GetMaterials()
        {
            return _context.MaterialMasters.ToList();
        }

        public IActionResult GetNextPONumber()
        {
            string nextPONumber;

            long lastPONumber = GetLastPONumberFromDatabase();

            if (lastPONumber != 0)
            {
                long lastNumber = lastPONumber + 1;
                nextPONumber = lastNumber.ToString();
            }
            else
            {
                nextPONumber = "1";
            }

            return Json(new { nextPONumber });
        }

        private long GetLastPONumberFromDatabase()
        {
            var lastPONumber = _context.PoHeadersMaster
                                 .OrderByDescending(p => p.OrderNumber)
                                 .Select(p => p.OrderNumber)
                                 .FirstOrDefault();

            return lastPONumber;
        }

        [HttpPost]
        public IActionResult Save(Po_header headerModel, string detailsJson)
        {
            if(detailsJson!=null)
            {
                List<Po_Details> detailsModels = JsonConvert.DeserializeObject<List<Po_Details>>(detailsJson);

                try
                {
                    long orderValue = (long)(detailsModels.Sum(d => d.ItemValue));

                    headerModel.OrderValue = (int)orderValue;
                    headerModel.OrderStatus = "Open";
                    _context.PoHeadersMaster.Add(headerModel);
                    _context.SaveChanges();

                    long orderId = headerModel.ID;

                    foreach (var detailsModel in detailsModels)
                    {
                        detailsModel.OrderID = (int)orderId;
                        _context.PoDetailsMaster.Add(detailsModel);
                    }

                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error saving data: " + ex.Message });
                }
            }
            else
            {
                ViewBag.error = "Please Add Entrsy in Order details. ";
                return View("Error1");
            }
        }
      

        public IActionResult Create()
        {
            var vendors = GetVendors();
            var selectList = new SelectList(vendors, "Id", "Name");
         
            ViewBag.VendorMaster = selectList;

            var materials = _context.MaterialMasters.ToList(); // Assuming you have a Materials DbSet in your DbContext
            ViewBag.Materials = new SelectList(materials, "Id", "Code");

            return View();
          

        }
      
    }
}
