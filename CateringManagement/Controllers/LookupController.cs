using CateringManagement.CustomControllers;
using CateringManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CateringManagement.Controllers
{
    public class LookupController : CognizantController
    {
        private readonly CateringContext _context;

        public LookupController(CateringContext context)
        {
            _context = context;
        }
        public IActionResult Index(string Tab = "Equipment-Tab")
        {
            ViewData["Tab"] = Tab;

            return View();
        }

        public PartialViewResult Equipment()
        {
            ViewData["EquipmentID"] = new
                SelectList(_context.Equipments
                .OrderBy(a => a.Name), "ID", "Name");
            return PartialView("_Equipment");
        }

        public PartialViewResult Room()
        {
            ViewData["RoomID"] = new
                SelectList(_context.Rooms
                .OrderBy(a => a.Summary), "ID", "Summary");
            return PartialView("_Room");
        }

        public PartialViewResult MealType()
        {
            ViewData["MealTypeID"] = new
                SelectList(_context.MealTypes
                .OrderBy(a => a.Name), "ID", "Name");
            return PartialView("_MealType");
        }

        public PartialViewResult Worker()
        {
            ViewData["WorkerID"] = new
                SelectList(_context.Workers
                .OrderBy(a => a.Summary), "ID", "Summary");
            return PartialView("_Worker");
        }

        public PartialViewResult FunctionType()
        {
            ViewData["FunctionTypeID"] = new
                SelectList(_context.FunctionTypes
                .OrderBy(a => a.Name), "ID", "Name");
            return PartialView("_FunctionType");
        }
    }
}
