using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegistruCentras.Domains;
using RegistruCentras.Web.Models;
using RegistruDomains.Data;

namespace RegistruCentras.Web.Controllers
{
    public class FaqController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        public FaqController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Faqs
        public async Task<IActionResult> Index()
        {
            var questioneer = await _userManager.GetUserAsync(User);
            return View(await _context.Faqs.Where(a => a.IsPrivate == false || a.AskedBy.Id==questioneer.Id).ToListAsync());

        }

        // GET: Faqs/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult AskQuestion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AskQuestion(QuestionViewModel model)
        {

            if (ModelState.IsValid)
            {
                var questioneer = await _userManager.GetUserAsync(User);
                var newQuestion = new Faq()
                {
                    AskedBy = questioneer, Question = model.Question, IsPrivate=model.IsPrivate
                };
                _context.Faqs.Add(newQuestion);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Question,Answer")]*/ FaqViewModel faq)
        {
            if (ModelState.IsValid)
            {
                Faq newFaq = new Faq()      //sukuriam faq objekta ir primapinam su gautu modeliu is view kuri galetumem saugot i DB
                {
                    Answer = faq.Answer,
                    Question = faq.Question
                };
                _context.Add(newFaq);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }

        // GET: Faqs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.Faqs.FindAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FaqID,Question,Answer")] Faq faq)
        {
            if (id != faq.FaqID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faq);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaqExists(faq.FaqID))
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
            return View(faq);
        }

        // GET: Faqs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.Faqs
                .FirstOrDefaultAsync(m => m.FaqID == id);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

        // POST: Faqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var faq = await _context.Faqs.FindAsync(id);
            _context.Faqs.Remove(faq);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaqExists(Guid id)
        {
            return _context.Faqs.Any(e => e.FaqID == id);
        }
    }
}
