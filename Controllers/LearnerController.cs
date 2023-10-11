using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LearnerController : Controller
    {
        private SchoolContext context;
        public LearnerController(SchoolContext schoolContext)
        {
            this.context = schoolContext;
        }
        public IActionResult Create()
        {
            ViewBag.MajorID = new SelectList(context.Majors, "MajorID", "MajorName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")]Learner learner)
        {
            if(ModelState.IsValid)
            {
                context.Learners.Add(learner);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MajorID = new SelectList(context.Majors, "MajorID", "MajorName");
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();  
            }

            Learner learner = context.Learners.Find(id);
            if (learner == null)
            {
                return NotFound();
            }
            ViewBag.MajorID = new SelectList(context.Majors, "MajorID", "MajorName");
            return View(learner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,[Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if(id!=learner.LearnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(learner);
                    context.SaveChanges();
                }catch (Exception ex)
                {
                    if(!LearnerExists(learner.LearnerID))
                    {
                        return NotFound(ex.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("Index");   
        }
        
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Learner learner=context.Learners
                .Include(l=>l.Major)
                .Include(e=>e.Enrollments)
                .FirstOrDefault(m=>m.LearnerID==id);
            if (learner == null)
            {
                return NotFound();
            }
            if(learner.Enrollments.Count()>0) {
                return Content("this learner has some enrollments!");
            }
            return View(learner);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken ]
        public IActionResult DeleteConfirmed(int id) {
            Learner learner = context.Learners.Find(id);
            if (learner == null) {  return NotFound(); }
            context.Remove(learner);
            context.SaveChanges();
            return RedirectToAction("Index");   
        }

        private bool LearnerExists(int id)
        {
            return context.Learners.Any(e=>e.LearnerID == id);
        }

        public IActionResult Index()
        {
            var learners=context.Learners.Include(m=>m.Major).ToList();
            return View(learners);
        }
    }
}
