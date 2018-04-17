using System;

namespace Fun.Api.DataContracts
{
    public class CreatePetRequest
    {
        int Id { get; set; }
        int CustomerId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Type { get; set; }
    }
}
