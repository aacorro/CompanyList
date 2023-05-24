using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactList.Data;
using ContactList.Models;
using CompaniesContactList.Repository;

namespace CompaniesContactList.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepository _compRepo;

        public CompaniesController(ICompanyRepository compRepo)
        {
            _compRepo = compRepo;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
              return View(_compRepo.GetAll());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _compRepo.Find == null)
            {
                return NotFound();
            }

            var company =  _compRepo.Find(id.GetValueOrDefault());
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,Name,Address,City,State,PostalCode")] Company company)
        {
            if (ModelState.IsValid)
            {
                _compRepo.Add(company);
                TempData["success"] = "Company added successfully";
                return RedirectToAction(nameof(Index));
            }
            
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _compRepo.Find == null)
            {
                return NotFound();
            }

            var company = _compRepo.Find(id.GetValueOrDefault());
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,Name,Address,City,State,PostalCode")] Company company)
        {

            if (id != company.CompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               _compRepo.Update(company);
                TempData["success"] = "Company updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _compRepo.Find == null)
        //    {
        //        return NotFound();
        //    }

        //    _compRepo.Remove(id.GetValueOrDefault());
        //    return RedirectToAction(nameof(Index));
            
        //}
        

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> allObj = _compRepo.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _compRepo.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _compRepo.Remove(id);
            return Json(new { success = true, message = "Company delete successful" });
        }

        #endregion

    }
}

