using atelier1.Models;
using atelier1.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace atelier1.Controllers
{

    public class EmployeeController : Controller
    {
        readonly IRepository<Employee> employeeRepository;
        //injection de dépendance
        public EmployeeController(IRepository<Employee> empRepository)
        {

            employeeRepository = empRepository;

        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            var employees = employeeRepository.GetAll();
            ViewData["EmployeesCount"] = employees.Count();
            ViewData["SalaryAverage"] = employeeRepository.SalaryAverage();
            ViewData["MaxSalary"] = employeeRepository.MaxSalary();
            ViewData["HREmployeesCount"] = employeeRepository.HrEmployeesCount();
            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            var employee = employeeRepository.FindByID(id);

            return View(employee);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            // Juste retourner la vue vide pour la création d'un nouvel employé
            return View();
        }


        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Ajouter le nouvel employé
                    employeeRepository.Add(employee);

                    // Rediriger vers la liste des employés après l'ajout
                    return RedirectToAction(nameof(Index));
                }
                // Si le modèle n'est pas valide, rester sur la vue de création
                return View(employee);
            }
            catch
            {
                // Gérer les erreurs éventuelles
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = employeeRepository.FindByID(id);

            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingEmployee = employeeRepository.FindByID(id);

                    if (existingEmployee != null)
                    {
                        // Mettez à jour les propriétés de l'employé existant
                        existingEmployee.Name = employee.Name;
                        existingEmployee.Departement = employee.Departement;
                        existingEmployee.Salary = employee.Salary;

                        // Mettez à jour dans le dépôt
                        employeeRepository.Update(id, existingEmployee);

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return View(employee);
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            var employee = employeeRepository.FindByID(id);

            return View(employee);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                var employeeToDelete = employeeRepository.FindByID(id);
                if (employeeToDelete != null)
                {
                    // Supprimer l'employé
                    employeeRepository.Delete(id);

                    // Rediriger vers la liste des employés après suppression
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                // Gérer les erreurs si quelque chose se passe mal
                return View();
            }
        }


        public ActionResult Search(string term)
        {

            var result = employeeRepository.Search(term);
            return View("Index", result);
        }
    }
}
