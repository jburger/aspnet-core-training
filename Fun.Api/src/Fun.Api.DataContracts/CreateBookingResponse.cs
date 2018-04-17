using System;
using System.Collections.Generic;

namespace Fun.Api.DataContracts
{
    public class CreateBookingResponse
    {
        int? Id { get; set; }
        decimal DepositRequired { get; set; }
        decimal TotalAmountOwing { get; set; }
        DateTime DueDate { get; set; }
    }
}
