using Compactor.Models.Domain;
using Compactor.Models.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Compactor.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var initialVm = PrepareInitialViewModel(userId);

            return View(initialVm);
        }

        private InitialViewModel PrepareInitialViewModel(string userId)
        {
            return new InitialViewModel
            {
                PositionList = new List<ReservationPosition>(),
                Reservation = new Reservation
                {
                    ID = 0,
                    UserID = userId,
                    RentDate = DateTime.Now,
                    ReservationPositions = new List<ReservationPosition>(),
                },
                EquipmentList = new List<Equipment> {
                    new Equipment 
                    {
                        ID = 1,
                        Name = "Wiertarka Bosh",
                        IsRented = false,
                        TypeID = 1 
                    }, 
                    new Equipment
                    {
                        ID = 2,
                        Name = "Ubijarka STANLEY",
                        IsRented = false,
                        TypeID = 2,
                    }
                },
                UserDataList = new List<UserData>
                {
                    new UserData
                    {
                        ID = 1,
                        Name = "Eustachy Motyka",
                        Email = "mail.eustachy@poczta",
                        UserID = userId,
                        Address = new Address
                        {
                            ID = 1,
                            Street = "ul. mała",
                            Number = "123a",
                            City = "Gniazdo",
                            PostalCode = "123-33"
                        }
                    }
                },
                GroupList = new List<EquipmentType>
                { 
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
                    }
                }

            };
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }
        public ActionResult ReservationList()
        {
            var reservationList = new List<Reservation>();
            for (int i = 0; i < 10; i++)
            {
                reservationList.Add(new Reservation
                {
                    Title = $"res nr. {i}",
                    RentDate = DateTime.Now,
                    ReturnDate = DateTime.MaxValue,
                    ID = i,
                    Value = i * 2 + 30,
                    UserData = new UserData { Name = "Eustachy Motyka" }
                });
            }
            return View(reservationList);
        }




    }
}