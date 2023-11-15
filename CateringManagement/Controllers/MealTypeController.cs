using CateringManagement.Data;
using CateringManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CateringManagement.Controllers
{
    public class MealTypeController : LookupsController
    {
        private readonly CateringContext _context;

        public MealTypeController(CateringContext context)
        {
            _context = context;
        }

        // GET: MealType
        public IActionResult Index()
        {
            return Redirect(ViewData["returnURL"].ToString());
        }

        // GET: MealType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MealType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] MealType mealType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(mealType);
                    await _context.SaveChangesAsync();
                    return Redirect(ViewData["returnURL"].ToString());
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(mealType);
        }

        // GET: MealType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MealTypes == null)
            {
                return NotFound();
            }

            var mealType = await _context.MealTypes.FindAsync(id);
            if (mealType == null)
            {
                return NotFound();
            }
            return View(mealType);
        }

        // POST: MealType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var mealTypeToUpdate = await _context.MealTypes.FirstOrDefaultAsync(r => r.ID == id);

            //Check that we got the function type or exit with a not found error
            if (mealTypeToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<MealType>(mealTypeToUpdate, "",
                    mt => mt.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return Redirect(ViewData["returnURL"].ToString());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealTypeExists(mealTypeToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(mealTypeToUpdate);
        }

        // GET: MealType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MealTypes == null)
            {
                return NotFound();
            }

            var mealType = await _context.MealTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mealType == null)
            {
                return NotFound();
            }

            return View(mealType);
        }

        // POST: MealType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MealTypes == null)
            {
                return Problem("Entity set 'CateringContext.MealTypes'  is null.");
            }
            var mealType = await _context.MealTypes.FindAsync(id);
            if (mealType != null)
            {
                _context.MealTypes.Remove(mealType);
            }

            await _context.SaveChangesAsync();
            return Redirect(ViewData["returnURL"].ToString());
        }

        private bool MealTypeExists(int id)
        {
            return _context.MealTypes.Any(e => e.ID == id);
        }
    }
}
