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
    public class StudentController : Controller
    {
        public readonly MVCAppContext _db;

        public StudentController(MVCAppContext db)
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

          public async Task<IActionResult> Index(string searchString)
        {
            var studobj = from t in _db.Student
                           select t;
            if(!String.IsNullOrEmpty(searchString))
            {
                studobj = studobj.Where(s => s.Name!.Contains(searchString));
            }

            return View(await studobj.ToListAsync());

        }

        //Get Create
        public IActionResult Create()
        {
            PopulateSubjectsDropDownList();
            return View();
        }


        [HttpPost]
        public IActionResult Create([Bind("Name,Class,SubjectId")] Student studobj)
        {
            
                _db.Student.Add(studobj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            

            return View(studobj);
        }

        //Get Create
        public IActionResult Edit(int id)
        {

            var studobj = _db.Student.Find(id);
            return View(studobj);
        }


        [HttpPost]
        public IActionResult Edit(Student obj)
        {
            
                _db.Student.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            
            // return View(obj);
        }

        //Get Delete
        public IActionResult Delete(int id)
        {

            var studobj = _db.Student.Find(id);
            return View(studobj);
        }


        [HttpPost]
        public IActionResult DeletePost(int studentid)
        {
            var studobj = _db.Student.Find(studentid);

            if (ModelState.IsValid)
            {

                _db.Student.Remove(studobj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studobj);
        }
    }
}
