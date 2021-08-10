using DemoBasketForReservation.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace DemoBasketForReservation.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public List<Personne> Personnes
        {
            get
            {
                return (!_session.Keys.Contains(nameof(Personnes))) ? null : new List<Personne>(JsonSerializer.Deserialize<Personne[]>(_session.GetString(nameof(Personnes))));
            }
            private set
            {
                _session.SetString(nameof(Personnes), JsonSerializer.Serialize(value));
            }
        }

        public void AddPersonne(Personne personne)
        {
            List<Personne> personnes = Personnes ?? new List<Personne>();
            personnes.Add(personne);
            Personnes = personnes;
        }

        public void RemovePersonne(int indice)
        {
            List<Personne> personnes = Personnes;

            if (personnes is null || personnes.Count() < indice)
                return;

            personnes.RemoveAt(indice);
            Personnes = personnes;
        }

        public void ResetBasket()
        {
            _session.Remove(nameof(Personnes));
        }
    }
}
