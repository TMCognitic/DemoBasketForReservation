using DemoBasketForReservation.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBasketForReservation.Models.Forms
{
    public class AddReservationForm
    {
        [DataType(DataType.Date)]
        [DateTimeRange]
        public DateTime Date { get; set; }

        public IEnumerable<Personne> Personnes { get; set; }

        public AddReservationForm()
        {
            Date = DateTime.Now;
        }
    }
}
