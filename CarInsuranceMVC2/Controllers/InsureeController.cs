using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsuranceMVC2.Models;

namespace CarInsuranceMVC2.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        //// GET: Insuree
        //public ActionResult Index()
        //{
        //    return View(db.Insurees.ToList());
        //}

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
                insuree.Quote = 50.00m;

                if (age < 18)
                {
                    insuree.Quote += 100.00m;
                }
                if (age >= 18 && age < 25)
                {
                    insuree.Quote += 25.00m;
                }
                if (age >= 100)
                {
                    insuree.Quote += 100.00m;
                }
                else if (age >= 25 && age < 100)
                {
                    insuree.Quote += 0;
                }

                if (insuree.CarYear < 2000)
                {
                    insuree.Quote += 25.00m;
                }
                if (insuree.CarYear > 2015)
                {
                    insuree.Quote += 25.00m;
                }
                else if (insuree.CarYear >= 2000 && insuree.CarYear <= 2015)
                {
                    insuree.Quote += 0;
                }
                if (insuree.CarMake == "Porche")
                {
                    insuree.Quote += 25.00m;
                    {
                        if (insuree.CarModel == "911 Carrera")
                        {
                            insuree.Quote += 25.00m;
                        }
                    }
                }
                else if (insuree.CarMake != "Porche")
                {
                    insuree.Quote += 0;
                }
                if (insuree.SpeedingTickets > 0)
                {
                    insuree.Quote += insuree.SpeedingTickets * 10.00m;
                }
                if (insuree.DUI == true)
                {
                    insuree.Quote = insuree.Quote + (insuree.Quote * .25m);
                }
                if (insuree.CoverageType == true)
                {
                    insuree.Quote = insuree.Quote + (insuree.Quote * .50m);
                }

                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Details", new { Id = insuree.Id });
            }

            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        //// GET: Insuree/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Insuree insuree = db.Insurees.Find(id);
        //    if (insuree == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(insuree);
        //}

        //// POST: Insuree/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Insuree insuree = db.Insurees.Find(id);
        //    db.Insurees.Remove(insuree);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
