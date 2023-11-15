using CateringManagement.CustomControllers;
using CateringManagement.Data;
using CateringManagement.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FunctionDocument = CateringManagement.Models.FunctionDocument;
using String = System.String;

namespace CateringManagement.Controllers
{
    public class FunctionDocumentController : ElephantController
    {
        private readonly CateringContext _context;

        public FunctionDocumentController(CateringContext context)
        {
            _context = context;
        }

        // GET: FunctionDocument
        public async Task<IActionResult> Index(string SearchString, int? page, int? pageSizeID)
        {
            ViewData["Filtering"] = "btn-outline-secondary";
            int numberFilters = 0;

            var functionDocument = _context.FunctionDocuments.Include(f => f.Function).AsNoTracking();

            //Add as many filters as needed            
            if (!String.IsNullOrEmpty(SearchString))
            {
                functionDocument = functionDocument.Where(d => d.FileName.ToUpper().Contains(SearchString.ToUpper()));
                numberFilters++;
            }
            //Give feedback about the state of the filters
            if (numberFilters != 0)
            {
                //Toggle the Open/Closed state of the collapse depending on if we are filtering
                ViewData["Filtering"] = " btn-danger";
                //Show how many filters have been applied
                ViewData["numberFilters"] = "(" + numberFilters.ToString()
                    + " Filter" + (numberFilters > 1 ? "s" : "") + " Applied)";
                //Keep the Bootstrap collapse open
                //@ViewData["ShowFilter"] = " show";
            }

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<FunctionDocument>.CreateAsync(functionDocument.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: FunctionDocument/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FunctionDocuments == null)
            {
                return NotFound();
            }

            var functionDocument = await _context.FunctionDocuments
                .Include(f => f.Function)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (functionDocument == null)
            {
                return NotFound();
            }

            return View(functionDocument);
        }

        // GET: FunctionDocument/Create
        public IActionResult Create()
        {
            ViewData["FunctionID"] = new SelectList(_context.Functions, "ID", "ID");
            return View();
        }

        // POST: FunctionDocument/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FunctionID,ID,FileName,MimeType")] FunctionDocument functionDocument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(functionDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FunctionID"] = new SelectList(_context.Functions, "ID", "ID", functionDocument.FunctionID);
            return View(functionDocument);
        }

        // GET: FunctionDocument/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FunctionDocuments == null)
            {
                return NotFound();
            }

            var functionDocument = await _context.FunctionDocuments.FindAsync(id);
            if (functionDocument == null)
            {
                return NotFound();
            }
            ViewData["FunctionID"] = new SelectList(_context.Functions, "ID", "ID", functionDocument.FunctionID);
            return View(functionDocument);
        }

        // POST: FunctionDocument/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FunctionID,ID,FileName,MimeType")] FunctionDocument functionDocument)
        {
            if (id != functionDocument.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(functionDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunctionDocumentExists(functionDocument.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FunctionID"] = new SelectList(_context.Functions, "ID", "ID", functionDocument.FunctionID);
            return View(functionDocument);
        }

        // GET: FunctionDocument/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FunctionDocuments == null)
            {
                return NotFound();
            }

            var functionDocument = await _context.FunctionDocuments
                .Include(f => f.Function)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (functionDocument == null)
            {
                return NotFound();
            }

            return View(functionDocument);
        }

        // POST: FunctionDocument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FunctionDocuments == null)
            {
                return Problem("Entity set 'CateringContext.FunctionDocuments'  is null.");
            }
            var functionDocument = await _context.FunctionDocuments.FindAsync(id);
            if (functionDocument != null)
            {
                _context.FunctionDocuments.Remove(functionDocument);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<FileContentResult> Download(int id)
        {
            var theFile = await _context.UploadedFiles
                .Include(d => d.FileContent)
                .Where(f => f.ID == id)
                .FirstOrDefaultAsync();
            return File(theFile.FileContent.Content, theFile.MimeType, theFile.FileName);
        }


        private bool FunctionDocumentExists(int id)
        {
            return _context.FunctionDocuments.Any(e => e.ID == id);
        }
    }
}
