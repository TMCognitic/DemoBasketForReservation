using DemoBasketForReservation.Models;
using System.Collections.Generic;

namespace DemoBasketForReservation.Infrastructure
{
    public interface ISessionManager
    {
        List<Personne> Personnes { get; }

        void AddPersonne(Personne personne);
        void RemovePersonne(int indice);
        void ResetBasket();
    }
}