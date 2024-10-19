using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamePlatform.Domain.Domain;
using GamePlatform.Repository;
using GamePlatform.Service.Interface;

namespace GamePlatform.Web.Controllers
{
    public class DevelopersController : Controller
    {
        private readonly IDeveloperService _devService;

        public DevelopersController(IDeveloperService devService)
        {
            _devService = devService;
        }

        // GET: Developers
        public IActionResult Index()
        {
            return View(_devService.GetAllDevelopers());
        }

        // GET: Developers/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developer = _devService.GetDeveloper(id);
            if (developer == null)
            {
                return NotFound();
            }

            return View(developer);
        }

        // GET: Developers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Developers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DevName,DevDesc,YearFormed,Id")] Developer developer)
        {
            if (ModelState.IsValid)
            {
                developer.Id = Guid.NewGuid();
                _devService.CreateDeveloper(developer);
                return RedirectToAction(nameof(Index));
            }
            return View(developer);
        }

        // GET: Developers/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developer = _devService.GetDeveloper(id);
            if (developer == null)
            {
                return NotFound();
            }
            return View(developer);
        }

        // POST: Developers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("DevName,DevDesc,YearFormed,Id")] Developer developer)
        {
            if (id != developer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _devService.UpdateDeveloper(developer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(developer);
        }

        // GET: Developers/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developer = _devService.GetDeveloper(id);
            if (developer == null)
            {
                return NotFound();
            }

            return View(developer);
        }

        // POST: Developers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _devService.DeleteDeveloper(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
