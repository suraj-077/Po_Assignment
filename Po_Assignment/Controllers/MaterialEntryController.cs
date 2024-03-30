using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Po_Assignment.Dal;
using Po_Assignment.Models;

namespace Po_Assignment.Controllers
{
    public class MaterialEntryController : Controller
    {
       

        private readonly ApplicationDbContext _context;

        public MaterialEntryController(ApplicationDbContext _context)
        {
            this._context = _context;

        }

        public IActionResult Index()
        {


            var data = _context.MaterialMasters;
            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                return View(data);
            }
          
        }


        [HttpGet]

        public IActionResult AddMaterial()
        {
            var lastMaterialData = _context.MaterialMasters.OrderByDescending(v => v.Code).FirstOrDefault();
            string newCode;


            if (lastMaterialData != null)
            {

                int lastNumericPart = int.Parse(lastMaterialData.Code.Substring(1));
                string newNumericPart = (lastNumericPart + 1).ToString("D3");
                newCode = "M" + newNumericPart;
            }
            else
            {

                newCode = "M001";
            }

            ViewBag.Code = newCode;
            return View();
        }

        //add or edit material data
        [HttpPost]
        public ActionResult AddMaterial(MaterialFormData materialData)
        {
            if (ModelState.IsValid)
            {
                var materialDataIsExists = _context.MaterialMasters.FirstOrDefault(c => c.Code == materialData.Code);

                if (materialDataIsExists == null)
                {
                    materialData.IsActive = true;
                    MaterialEntry materialEntry = new MaterialEntry();

                    // Assign values from materialData to materialEntry
                    materialEntry.Code = materialData.Code;
                    materialEntry.ShortText = materialData.ShortText;
                    materialEntry.LongText = materialData.LongText;
                    materialEntry.Reorder_Level = materialData.Reorder_Level;
                    materialEntry.Min_Order_Quantity = materialData.Min_Order_Quantity;
                    materialEntry.Unit = materialData.Unit;
                    materialEntry.IsActive = materialData.IsActive;

                    _context.MaterialMasters.Add(materialEntry);
                }
                else
                {
                    // Update existing material entry
                    materialDataIsExists.ShortText = materialData.ShortText;
                    materialDataIsExists.LongText = materialData.LongText;
                    materialDataIsExists.Reorder_Level = materialData.Reorder_Level;
                    materialDataIsExists.Min_Order_Quantity = materialData.Min_Order_Quantity;
                    materialDataIsExists.Unit = materialData.Unit;
                    materialDataIsExists.IsActive = materialData.IsActive;
                }

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                // If ModelState is not valid, return appropriate response
                return BadRequest(ModelState);
            }
        }


        [HttpGet]
        // update existing vendor
        public IActionResult Edit(int? Id)
        {
            if (Id < 0)
            {
                return BadRequest();
            }

            MaterialEntry data = _context.MaterialMasters.FirstOrDefault(a => a.Id == Id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

      /*  [HttpPost]
        public IActionResult Edit(int id,MaterialEntry materialData)
        {
            if (id != materialData.Id)
            {
                return BadRequest();
            }

            _context.MaterialMasters.Update(materialData);
            _context.SaveChanges();

            // TempData["SuccessMessage"] = "Vendor details updated successfully!";
            return RedirectToAction("Index");
        }*/

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var material = _context.MaterialMasters.Find(Id);

            return View(material);
        }

        [HttpPost]
        //delete existing vendor
        public IActionResult Delete(int? Id)
        {
            if (Id < 0)
            {
                return BadRequest();
            }

            MaterialEntry data = _context.MaterialMasters.Find(Id);

            if (data == null)
            {
                return BadRequest();
            }

            string name = data.Code;
            _context.MaterialMasters.Remove(data);
            _context.SaveChanges();
            TempData["ErrorMessage"] = $"Material {name} deleted successfully!";

            return RedirectToAction("Index");
        }



    }
}
