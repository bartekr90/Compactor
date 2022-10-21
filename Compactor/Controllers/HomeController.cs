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
        private readonly DeviceRepository _deviceRepository = new DeviceRepository();

        public ActionResult Index(bool modelGood = true)
        {
            var userId = User.Identity.GetUserId();

            List<UserData> userDataList = _userDataRepository.GetListOfData(userId);

            if (!Utils.IsAny(userDataList))
                userDataList.Add(_userDataRepository.PrepareFirstEntity(userId));

            List<EquipmentType> eqTypeList = _typeRepository.GetListOfTypes();

            return View(PrepareInitialVM(userId, userDataList, eqTypeList, modelGood));
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
            var userID = User.Identity.GetUserId();

            if (reservation.UserID != userID)
                return RedirectToAction("Index");

            reservation.ReservationPositions = GetCartSession();

            if (ModelState.IsValid && Utils.IsAny(reservation.ReservationPositions))
            {
                if (reservation.AssignValue(Convert.ToDecimal(reservation.GetHours())))
                {
                    TempData["reservation"] = reservation;
                    return View("Reservation", reservation);
                }
            }
            return RedirectToAction("Index", new { modelGood = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReservation()
        {
            UpdateCartSession(new List<ReservationPosition>());
            Reservation reservation = (Reservation)TempData["reservation"];

            var userID = User.Identity.GetUserId();
            if (reservation == null || !Utils.IsAny(reservation.ReservationPositions) || reservation.UserID != userID)
                return RedirectToAction("Index", new { modelGood = false });

            if (!reservation.PreparePositionsToSave())
                return RedirectToAction("Index", new { modelGood = false });

            reservation.IsActiv = true;
            _reservationRepository.Add(reservation);
            _deviceRepository.UpdateStates(reservation.ReservationPositions);
            _typeRepository.UpdateBorrowedNr(reservation.ReservationPositions, UpdateMode.Add);

            return RedirectToAction("ReservationList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CloseReservation(int id)
        {
            var userID = User.Identity.GetUserId();

            var list = _reservationRepository.CloseReservation(userID, id);

            if (Utils.IsAny(list))
            {
                _deviceRepository.UpdateStates(list);
                _typeRepository.UpdateBorrowedNr(list, UpdateMode.Subtract);
            }

            return RedirectToAction("ReservationList");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OnAddPosition(int id)
        {
            List<ReservationPosition> cartSession = GetCartSession();
            Device device = _deviceRepository.GetFirstEqWithIdOf(id);

            if (device == null)
                return PartialView("_ReservationPositions", cartSession);

            var userID = User.Identity.GetUserId();
            var position = new ReservationPosition(device, cartSession, userID);
            cartSession.Add(position);
            
            if (!device.Type.IsInStock(cartSession))
            {
                cartSession.Remove(position);
                return PartialView("_ReservationPositions", cartSession);
            }

            UpdateCartSession(cartSession);
            return PartialView("_ReservationPositions", cartSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OnDeletePosition(int i)
        {
            List<ReservationPosition> cartSession = GetCartSession();
            var length = cartSession.Count();

            if (i < 0 && i > length)
                return PartialView("_ReservationPositions", cartSession);

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
        private InitialViewModel PrepareInitialVM(string userId, List<UserData> userDataList, List<EquipmentType> eqTypeList, bool modelGood)
        {
            if (modelGood)
                return new InitialViewModel(
                                new ReservationViewModel(
                                    new Reservation(
                                        userId,
                                        GetCartSession()),
                                    userDataList),
                                eqTypeList);
            else
                return new InitialViewModel(
                                 new ReservationViewModel(
                                     new Reservation(
                                         userId,
                                         GetCartSession()),
                                     userDataList),
                                 eqTypeList)
                {
                    Information = "Wymagane pola: - Pozycje rezerwacji -- Data wypożycznenia -- Data zwrotu -- Rezerwacja na nazwisko -"
                };
        }
    }
}