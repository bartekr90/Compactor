using Compactor.Models.Domain;
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
            return View();
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
                reservationList.Add(new Reservation {
                    Title = $"res nr. {i}",
                    RentDate = DateTime.Now,
                    ReturnDate = DateTime.MaxValue,
                    Id = i,
                    Value = i*2+30,
                    UserData = new UserData { Name = "Eustachy Motyka"}
                    });
            }
            return View(reservationList);
        }
    }
}