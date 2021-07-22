using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {

            using (var context = new Data.DataModelContext()) {

                // Load users from database
                var userEntities = context.Users.OrderBy(o => o.Name).ToList();

                // Get a list of all the companies
                var companyIDs = userEntities.Select(s => s.WorksForCompanyID).Distinct().ToList();

                // Load all of the companies
                var companyEntities = context.Companies.Where(w => companyIDs.Contains(w.CompanyID)).ToList();

                // Create the view models from the entity data.
                var viewModels = userEntities.Select(s => new Models.UserModel {
                    UserID = s.UserID,
                    Name = s.Name,
                    Email = s.Email,
                    CompanyID = s.WorksForCompanyID,
                    Company = companyEntities.FirstOrDefault(e => e.CompanyID == s.WorksForCompanyID)?.Name
                });

                return View(viewModels);
            }
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            using (var context = new Data.DataModelContext()) {

                // Load the user from the database
                var userEntity = context.Users.FirstOrDefault(e => e.UserID == id);

                // Get a list of all the companies
                var companyEntities = context.Companies.OrderBy(o => o.Name).ToList();

                // Create the view models from the entity data.
                var viewModel = new Models.UserModel {
                    UserID = userEntity.UserID,
                    Name = userEntity.Name,
                    Email = userEntity.Email,
                    CompanyID = userEntity.WorksForCompanyID,
                    Company = companyEntities.FirstOrDefault(e => e.CompanyID == userEntity.WorksForCompanyID)?.Name
                };

                // Populate the ViewBag with the dropdown options for company
                ViewBag.Companies = companyEntities.Select(s => new SelectListItem { Text = s.Name, Value = s.CompanyID.ToString() }).ToList();
                
                return View(viewModel);
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Models.UserModel viewModel)
        {
            try
            {
                using (var context = new Data.DataModelContext()) {

                    // Load the user from the database
                    var userEntity = context.Users.FirstOrDefault(e => e.UserID == id);

                    userEntity.Name = viewModel.Name;
                    userEntity.Email = viewModel.Email;
                    userEntity.WorksForCompanyID = viewModel.CompanyID;

                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
