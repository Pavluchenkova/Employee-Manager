using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeManager.Models;
using EmployeeManager.ViewModels;

namespace EmployeeManager.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeeManagerContext db = new EmployeeManagerContext();

        // GET: /Employees/
        public ActionResult Index()
        {
            List<EmployeeViewModel> models = new List<EmployeeViewModel>();

            foreach (var item in db.Employees)
            {
                models.Add(ConvertEmployeeToViewModel(item));
            }
            return View(models);
        }

        // GET: /Employees/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var model = ConvertEmployeeToViewModel(employee);
            return View(model);
        }

        // GET: /Employees/Create
        public ActionResult Create()
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model.SelectEmployeeList = CreateSelectEmployeeList(db.Employees);

            return View(model);
        }

        // POST: /Employees/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,ManagerId,Name,LastName,Email,Position,SelectEmployeeList")] EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SelectEmployeeList = CreateSelectEmployeeList(db.Employees);
   
                return View(model);
            }

            var employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                Email = model.Email,
                LastName = model.LastName,
                Name = model.Name,
                ManagerId = model.ManagerId,
                Position = model.Position
            };

            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: /Employees/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var model = ConvertEmployeeToViewModel(employee);

            model.SelectEmployeeList = CreateSelectEmployeeList(db.Employees.Where(e => e.EmployeeId != id)); ;

            return View(model);
        }

        // POST: /Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,ManagerId,Name,LastName,Email,Position")] EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var employee = new Employee()
            {
                EmployeeId = model.EmployeeId,
                Email = model.Email,
                LastName = model.LastName,
                Name = model.Name,
                ManagerId = model.ManagerId,
                Position = model.Position
            };
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: /Employees/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var model = ConvertEmployeeToViewModel(employee);
            return View(model);
        }

        // POST: /Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
         
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private EmployeeViewModel ConvertEmployeeToViewModel(Employee employee)
        {
            var managerName = String.Empty;
            if (employee.ManagerId.HasValue)
            {
                Employee manager = db.Employees.Find(employee.ManagerId);
                managerName = String.Format("{0} {1}", manager.Name, manager.LastName);
            }
            return new EmployeeViewModel()
                 {
                     EmployeeId = employee.EmployeeId,
                     Email = employee.Email,
                     LastName = employee.LastName,
                     Name = employee.Name,
                     ManagerId = employee.ManagerId,
                     Manager = managerName,
                     Position = employee.Position
                 };
        }
        private List<SelectListItem> CreateSelectEmployeeList(IEnumerable<Employee> collection)
        {
          var selectEmployeeList = new List<SelectListItem>();
          selectEmployeeList.Add(new SelectListItem { Text = String.Empty, Value = null });

            foreach (var item in db.Employees)
            {
                selectEmployeeList.Add(new SelectListItem
                {
                    Text = String.Format("{0} {1}", item.Name, item.LastName),
                    Value = item.EmployeeId.ToString()
                });
            }
            return selectEmployeeList;
        }

    }
}
