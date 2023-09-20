using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces;
using webapi.Models.DND;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllyAndOrganizationController : GenericController<AllyAndOrganization>
    {
        public AllyAndOrganizationController(IGenericService<AllyAndOrganization> service) : base(service)
        {
        }
    }
}
