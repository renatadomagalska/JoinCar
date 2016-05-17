using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using JoinCar.Database;
using JoinCar.Database.Entities;
using JoinCar.Database.Repositories.Interfaces;
using JoinCar.Database.Repositories.Repositories;
using JoinCar.Helpers;
using Microsoft.AspNet.Identity;

namespace JoinCar.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsRepository _tripsRepository;
        private readonly IOpinionsRepository _opinionsRepository;
        private readonly IInterestsRepository _interestsRepository;

        public TripsController()
        {
            _tripsRepository = new TripsRepository();
            _opinionsRepository = new OpinionsRepository();
            _interestsRepository = new InterestsRepository();
        }

        public TripsController(ITripsRepository tripsRepository, IOpinionsRepository opinionsRepository,
            IInterestsRepository interestsRepository)
        {
            _tripsRepository = tripsRepository;
            _opinionsRepository = opinionsRepository;
            _interestsRepository = interestsRepository;
        }

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
            var objectsList = new List<object>
            {
                trip,
                _tripsRepository.GetTripPassengers(id.Value),
                _interestsRepository.GetInterestsByUserId(User.Identity.GetUserId())
            };
            return View(objectsList);
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
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _tripsRepository.DeleteTrip(id.Value);
            _tripsRepository.Save();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult MyTrips()
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
        public ActionResult MyInterests()
        {
            return View(_interestsRepository.GetInterestsByUserId(User.Identity.GetUserId()).Select(i => i.Trip));
        }

        [Authorize]
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "JoinCar")]
        [ValidateAntiForgeryToken]
        public ActionResult JoinCar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var scope = new TransactionScope())
            {
                var interest = new Interest()
                {
                    TripId = id.Value,
                    UserId = User.Identity.GetUserId()
                };
                _interestsRepository.AddInterest(interest);
                _interestsRepository.Save();

                _tripsRepository.DecrementAvailableSeats(id.Value);
                _tripsRepository.Save();

                scope.Complete();
            }

            return RedirectToAction("MyInterests");
        }

        [Authorize]
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "LeaveCar")]
        [ValidateAntiForgeryToken]
        public ActionResult LeaveCar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var scope = new TransactionScope())
            {
                var interest = _interestsRepository.GetInterestByTripAndUserIds(id.Value, User.Identity.GetUserId());
                _interestsRepository.DeleteInterest(interest.Id);
                _interestsRepository.Save();

                _tripsRepository.IncrementAvailableSeats(id.Value);
                _tripsRepository.Save();

                scope.Complete();
            }

            return RedirectToAction("MyInterests");
        }

        [Authorize]
        public ActionResult CreateOpinion(int? tripId)
        {
            if (tripId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
            var objectsList = new List<object>
            {
                trip,
                _tripsRepository.GetTripPassengers(trip.Id),
                _interestsRepository.GetInterestsByUserId(User.Identity.GetUserId())
            };
            return View("Details", objectsList);
        }
    }
}
