using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fun.Api.DataContracts;

namespace Fun.Api.Host.Controllers
{
    public class PetController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new List<Pet> {
                new Pet() { Id = 1, Name = "Poohbah", Type = new PetType() {Id = 1, Name = "Dog"}}
            });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(new List<Pet> {
                new Pet() { Id = id, Name = "Poohbah", Type = new PetType() {Id = 1, Name = "Dog"}}
            });
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Pet pet)
        {
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Pet value)
        {
            return Ok();
        }

        // DELETE api/values/5
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
