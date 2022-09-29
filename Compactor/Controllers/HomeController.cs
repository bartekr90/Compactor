using Compactor.Models;
using Compactor.Models.Domain;
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
        readonly List<EquipmentType> eqTypeList;
        readonly List<UserData> userDataList;

        public HomeController()
        {
            userDataList = new List<UserData>
            {
                new UserData
                {
                    ID = 1,
                    Email = "jakis@mail.com",
                    Name = "Eustachy Motyka",
                    UserID = "1",
                    AddressID = 1
                },
                new UserData
                {
                    ID = 2,
                    Email = "wiec@mail.com",
                    Name = "Ela Motyka",
                    UserID = "1",
                    AddressID = 1
                },
                new UserData
                {
                    ID = 3,
                    Email = "tpjest@mail.com",
                    Name = "Franek Kimono",
                    UserID = "1",
                    AddressID = 2
                },
                new UserData
                {
                    ID = 4,
                    Email = "costamtam@mail.com",
                    Name = "Kazik Buc",
                    UserID = "2",
                    AddressID = 3
                }
            };

            eqTypeList = new List<EquipmentType> {
                    new EquipmentType
                    {
                        ID = 1,
                        Name = "Wiertarka",
                        BorrowedNumber = 0,
                        TotalNumber = 1,
                        Price = 30m
                    },
                    new EquipmentType
                    {
                        ID = 2,
                        Name = "Ubijarka",
                        BorrowedNumber = 0,
                        TotalNumber = 1,
                        Price = 120m
                    },
                    new EquipmentType
                    {
                        ID = 3,
                        Name = "kosiarka",
                        BorrowedNumber = 0,
                        TotalNumber = 2,
                        Price = 40m
                    },
                    new EquipmentType
                    {
                        ID = 4,
                        Name = "Piła",
                        BorrowedNumber = 0,
                        TotalNumber = 4,
                        Price = 100m
                    }
            };
        }

       
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();


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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrepareReservation(Reservation reservation)
        {
            reservation.ReservationPositions = GetCartSession();
            if (ModelState.IsValid && Utils.IsAny(reservation.ReservationPositions))
            {
                if (reservation.AssignValue(Convert.ToDecimal(reservation.GetHours())))
                    return View("ConfirmReservation", reservation);

                //dodać werywikacje wszystkich property w reservation
                //dodanie reserwacji do temp data
            }

            return RedirectToAction("Index", reservation);
            //mozna zrobić przekazanie reservation do index, ale musi byc nullable
            //przygotowanie akcji post z dodaniem rezerwacji do bazy
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OnAddPosition(int id)
        {
            EquipmentType eqType = eqTypeList.FirstOrDefault(x => x.ID == id);
            List<ReservationPosition> cartSession = GetCartSession();

            if (eqType.IsInStock())
            {
                var position = new ReservationPosition(eqType, cartSession);
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