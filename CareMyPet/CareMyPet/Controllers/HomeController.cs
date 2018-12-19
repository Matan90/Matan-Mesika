using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareMyPet.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [ActionName("Stores")]
        public List<Store> GetStores()
        {
            Store store = new Store();
            return store.GetStores();
        }

        [HttpGet]
        [ActionName("VS")]
        public List<VeterinaryServices> GetVS()
        {
            VeterinaryServices vs = new VeterinaryServices();
            return vs.GetVS();
        }

    }
}