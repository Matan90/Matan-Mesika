using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareMyPet.Controllers
{
    [Route("api/Stores")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        [HttpGet]
        public List<Store> GetStoresList()
        {
            Store store = new Store();
            return store.GetStores();
        }
    }
}