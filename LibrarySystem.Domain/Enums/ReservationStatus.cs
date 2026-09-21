using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Domain.Enums
{
    public enum ReservationStatus
    {
        Pending,      // Står i kö
        //Ready,        // boken finns att hämta
        Fulfilled,    // Medlemmen har lånat boken
        Cancelled,    // Medlemmen avbröt
        //Expired       // Medlemmen hämtade inte i tid
    }
}
