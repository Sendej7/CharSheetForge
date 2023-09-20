using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces;
using webapi.Models.DND;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : GenericController<Equipment>
    {
        public EquipmentController(IGenericService<Equipment> service) : base(service)
        {
        }
    }
}
