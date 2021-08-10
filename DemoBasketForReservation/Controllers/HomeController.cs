using Cognitic.Tools.Connections.Database;
using DemoBasketForReservation.Infrastructure;
using DemoBasketForReservation.Models;
using DemoBasketForReservation.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBasketForReservation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISessionManager _sessionManager;
        private readonly Connection _connection;

        public HomeController(ILogger<HomeController> logger, ISessionManager sessionManager, Connection connection)
        {
            _logger = logger;
            _sessionManager = sessionManager;
            _connection = connection;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Reservation()
        {
            return View(new AddReservationForm() { Personnes = _sessionManager.Personnes });
        }

        [HttpPost]
        public IActionResult Reservation(AddReservationForm form)
        {
            if(!ModelState.IsValid)
            {
                form.Personnes = _sessionManager.Personnes;
                return View(form);
            }

            if(_sessionManager.Personnes is null || _sessionManager.Personnes.Count() == 0)
            {
                ModelState.AddModelError("", "Ther is no guest in your reservation...");
                form.Personnes = _sessionManager.Personnes;
                return View(form);
            }

            DataTable guests = new DataTable();
            guests.Columns.Add("LastName", typeof(string));
            guests.Columns.Add("FirstName", typeof(string));

            foreach(Personne personne in _sessionManager.Personnes)
            {
                guests.Rows.Add(personne.LastName, personne.FirstName);
            }

            Command command = new Command("BFSP_AddReservation", true);
            command.AddParameters("Date", form.Date);
            command.AddParameters("Guests", guests);
            _connection.ExecuteNonQuery(command);
            _sessionManager.ResetBasket();
            return RedirectToAction("Index");
        }

        public IActionResult RemoveGuest(int id)
        {
            _sessionManager.RemovePersonne(id);
            return PartialView("_Guests", _sessionManager.Personnes);
        }

        [HttpPost]
        public IActionResult AddGuest(Personne personne)
        {
            _sessionManager.AddPersonne(personne);
            return PartialView("_Guests", _sessionManager.Personnes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
