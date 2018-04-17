using System;
using System.Collections.Generic;

namespace Fun.Api.DataContracts
{
    public class CreateBookingRequest
    {
        string StartDate { get; set; }
        string EndDate { get; set; }
        
        int CustomerId { get; set; }
        List<int> Pets { get; set; }
    }
}
