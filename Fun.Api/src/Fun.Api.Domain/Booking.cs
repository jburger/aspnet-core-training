using System;
using System.Collections.Generic;

namespace Fun.Api.DataContracts
{
    public class Booking
    {
        int Id { get; set; }
        decimal DepositPercentage { get; set; }
        decimal TotalAmountOwing { get; set; }
        DateTime DueDate { get; set; }

        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        
        Customer Customer { get; set; }
        List<Pet> Pets { get; set; }
    }
}
