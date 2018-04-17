using System;
using System.Collections.Generic;
namespace Fun.Api.DataContracts
{
    public class Customer
    {
        int Id { get; set; }
        string Name { get; set; }
        List<Pet> Pets { get; set; }
    }
}
