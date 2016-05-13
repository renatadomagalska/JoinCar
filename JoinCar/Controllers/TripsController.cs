using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JoinCar.Database;
using JoinCar.Database.Entities;
using JoinCar.Database.Repositories.Interfaces;
using JoinCar.Database.Repositories.Repositories;

namespace JoinCar.Controllers
{
    public class TripsController : Controller
    {
        private ITripsRepository _tripsRepository = new TripsRepository();

        // GET: Trips
        public ActionResult Index()
        {
            return View(_tripsRepository.GetAllActiveTrips());
        }

        // GET: Trips
        public ActionResult ArchivedTripsList()
        {
            return View(_tripsRepository.GetAllArchivedTrips());
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = _tripsRepository.GetTripById(id.Value);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,From,To,DateTime,AvailableSeats,Description")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                _tripsRepository.AddTrip(trip);
                _tripsRepository.Save();
                return RedirectToAction("Index");
            }

            return View(trip);
        }

        // GET: Trips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = _tripsRepository.GetTripById(id.Value);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,From,To,DateTime,AvailableSeats,Description")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                _tripsRepository.EditTrip(trip);
                _tripsRepository.Save();
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = _tripsRepository.GetTripById(id.Value);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _tripsRepository.DeleteTrip(id);
            _tripsRepository.Save();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
