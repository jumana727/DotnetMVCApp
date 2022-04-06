using MVCApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCApp.Data;
using MVCApp.Models;


namespace MVCApp.Controllers
{
    public class TeacherController : Controller
    {
        public readonly MVCAppContext _db;

        public TeacherController(MVCAppContext db)
        {
            _db = db;
        }

        private void PopulateSubjectsDropDownList(object selectedSubject = null)
        {
            var subjectsQuery = from s in _db.Subject
                                orderby s.Subject_Name
                                select new { SubjectId = s.ID, s.Subject_Name };

            ViewBag.SubjectID = new SelectList(subjectsQuery.AsNoTracking(), "SubjectId", "Subject_Name", selectedSubject);

        }

        public IActionResult Create()
        {
            PopulateSubjectsDropDownList();
            return View();
        }


        [HttpPost]
        public IActionResult Create([Bind("Name,TeacherId")] Teacher teacherobj)
        {
            
                _db.Teacher.Add(teacherobj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            

            return View(teacherobj);
        }
        

        // public IActionResult Index()
        // {
            
        //     // IEnumerable<Teacher> objList = _db.Teacher;
        //     // return View(objList);
        // }
        public async Task<IActionResult> Index(string searchString)
        {
            var teachers = from t in _db.Teacher
                           select t;
            if(!String.IsNullOrEmpty(searchString))
            {
                teachers = teachers.Where(s => s.Name!.Contains(searchString));
            }

            return View(await teachers.ToListAsync());

        }
    }
}
