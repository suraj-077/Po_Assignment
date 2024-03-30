using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Po_Assignment.Dal;
using Po_Assignment.Models;
using System.Collections.Generic;
using System.Linq;

namespace Po_Assignment.Controllers
{
    public class PoDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PoDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

      

        private List<MaterialEntry> GetMaterial()
        {
            return _context.MaterialMasters.ToList();
        }

        public IActionResult PartialPoDetails()
        {
            var materials = GetMaterial();
            ViewBag.MaterialID = new SelectList(materials, "Id", "Code");
            return View();
        }

        public IActionResult GetMaterialDetails(long materialId)
        {
            var material = _context.MaterialMasters.FirstOrDefault(m => m.Id == materialId);
            if (material != null)
            {
                return Json(new { shortText = material.ShortText, unit = material.Unit });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
