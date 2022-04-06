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
    public class SubjectController : Controller
    {
        public readonly MVCAppContext _db;

        public SubjectController(MVCAppContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var studobj = from t in _db.Subject
                           select t;
            if(!String.IsNullOrEmpty(searchString))
            {
                studobj = studobj.Where(s => s.Subject_Name!.Contains(searchString));
            }

            return View(await studobj.ToListAsync());

        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create([Bind("Subject_Name,syllabus,credits")] Subject studobj)
        {

            _db.Subject.Add(studobj);
            _db.SaveChanges();
            return RedirectToAction("Index");


            return View(studobj);
        }


        [HttpGet]
        public IActionResult Edit(int subjectid)
        {
            var subobj = _db.Subject.Find(subjectid);
            return View(subobj);

        }

        [HttpPost]
        public IActionResult Edit(Subject updatedvaluesobj)
        {
            _db.Subject.Update(updatedvaluesobj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
