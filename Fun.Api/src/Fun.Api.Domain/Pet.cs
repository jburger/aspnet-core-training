using System;

namespace Fun.Api.DataContracts
{
    public class Pet
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PetType Type { get; set; }
    }

    public class PetType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
