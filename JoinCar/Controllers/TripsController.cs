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
using Microsoft.AspNet.Identity;

namespace JoinCar.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsRepository _tripsRepository = new TripsRepository();
        private readonly IOpinionsRepository _opinionsRepository = new OpinionsRepository();
        private readonly IInterestsRepository _interestsRepository = new InterestsRepository();
        private readonly IApplicationUsersRepository _applicationUsersRepository = new ApplicationUsersRepository();

        // GET: Trips
        public ActionResult Index(string searchStringFrom, string searchStringTo, DateTime? searchStartDate,
            DateTime? searchEndDate)
        {
            return
                View(_tripsRepository.GetActiveFilteredTrips(searchStringFrom, searchStringTo, searchStartDate,
                    searchEndDate));
        }

        // GET: Trips
        public ActionResult ArchivedTripsList(string searchStringFrom, string searchStringTo, DateTime? searchStartDate,
            DateTime? searchEndDate)
        {
            return
                View(_tripsRepository.GetArchivedFilteredTrips(searchStringFrom, searchStringTo, searchStartDate,
                    searchEndDate));
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
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,From,To,DateTime,AvailableSeats,Description,JsonDirections")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                trip.UserId = User.Identity.GetUserId();
                _tripsRepository.AddTrip(trip);
                _tripsRepository.Save();
                return RedirectToAction("Index");
            }

            return View(trip);
        }

        // GET: Trips/Edit/5
        [Authorize]
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
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,From,To,DateTime,AvailableSeats,Description,JsonDirections")] Trip trip)
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
        [Authorize]
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
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _tripsRepository.DeleteTrip(id);
            _tripsRepository.Save();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult UserTrips()
        {
            var userId = User.Identity.GetUserId();
            var objectsList = new List<object>
            {
                _tripsRepository.GetUserTrips(userId),
                _opinionsRepository.GetUserReceivedOpinions(userId),
                _opinionsRepository.GetUserIssuedOpinions(userId)
            };
            return View(objectsList);
        }

        [Authorize]
        public ActionResult UserInterests()
        {
            return View(_interestsRepository.GetInterestsByUserId(User.Identity.GetUserId()).Select(i => i.Trip));
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserInterests(int tripId)
        {
            var interest = new Interest(){
                TripId = tripId,
                UserId = User.Identity.GetUserId()
            };
            _interestsRepository.AddInterest(interest);
            _interestsRepository.Save();

            return RedirectToAction("UserInterests");
        }

        [Authorize]
        public ActionResult CreateOpinion(int tripId)
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOpinion([Bind(Include = "Id,Description,TripId")] Opinion opinion)
        {
            if (ModelState.IsValid)
            {
                opinion.UserIssuingOpinionId = User.Identity.GetUserId();
                _opinionsRepository.AddOpinion(opinion);
                _opinionsRepository.Save();
            }
            var trip = _tripsRepository.GetTripById(opinion.TripId);
            return View("Details", trip);
        }
    }
}
