using Compactor.Models;
using Compactor.Models.Domain;
using Compactor.Models.Repositories;
using Compactor.Models.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Compactor.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserDatasRepository _userDataRepository = new UserDatasRepository();
        private readonly EquipmentTypeRepository _typeRepository = new EquipmentTypeRepository();
        private readonly ReservationRepository _reservationRepository = new ReservationRepository();
        private readonly EquipmentRepository _equipmentRepository = new EquipmentRepository();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            List<UserData> userDataList = _userDataRepository.GetListOfData(userId);
            List<EquipmentType> eqTypeList = _typeRepository.GetListOfTypes();

            //Reservation reservation = id == 0 ? GetNewReservation(userId) : _invoiceRepository.GetInvoice(id, userId);


            return View(
                new InitialViewModel(
                    new ReservationViewModel(
                        new Reservation(
                            userId,
                            GetCartSession()),
                        userDataList),
                    eqTypeList));
        }

        public ActionResult ReservationList()
        {
            var userId = User.Identity.GetUserId();

            var reservationList = _reservationRepository.GetListofReservations(userId);

            return View(reservationList);
        }

        public ActionResult Reservation(int id)
        {
            var userId = User.Identity.GetUserId();

            return View(_reservationRepository.GetReservation(userId, id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrepareReservation(Reservation reservation)
        {
            reservation.ReservationPositions = GetCartSession();
            if (ModelState.IsValid && Utils.IsAny(reservation.ReservationPositions))
            {
                if (reservation.AssignValue(Convert.ToDecimal(reservation.GetHours())))
                {
                    TempData["reservation"] = reservation;
                    UpdateCartSession(new List<ReservationPosition>());
                    return View("Reservation", reservation);
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddReservation()
        {
            Reservation reservation = (Reservation)TempData["reservation"];
            var userID = User.Identity.GetUserId();

            if (reservation == null || !Utils.IsAny(reservation.ReservationPositions) || reservation.UserID != userID)
                return RedirectToAction("Index");

            if (reservation.PreparePositionsToSave())
            {
                _reservationRepository.Add(reservation);
                _equipmentRepository.UpdateStates(reservation.ReservationPositions);
                _typeRepository.UpdateBorrowedNr(reservation.ReservationPositions);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OnAddPosition(int id)
        {
            List<ReservationPosition> cartSession = GetCartSession();
            Equipment equipment = _equipmentRepository.GetFirstEqWithIdOf(id);

            if (equipment.Type.IsInStock())
            {
                var position = new ReservationPosition(equipment, cartSession);
                cartSession.Add(position);
                UpdateCartSession(cartSession);
                return PartialView("_ReservationPositions", cartSession);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OnDeletePosition(int i)
        {
            List<ReservationPosition> cartSession = GetCartSession();
            var length = cartSession.Count();

            if (i < 0 && i > length)
                return RedirectToAction("Index");

            cartSession.RemoveAt(i);
            UpdateCartSession(cartSession);

            return PartialView("_ReservationPositions", cartSession);
        }


        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }


        private void UpdateCartSession(List<ReservationPosition> list)
        {
            Session["Cart"] = list;
        }
        private List<ReservationPosition> GetCartSession()
        {
            if (Session["Cart"] != null)
                return (List<ReservationPosition>)Session["Cart"];
            return new List<ReservationPosition>();
        }



    }
}