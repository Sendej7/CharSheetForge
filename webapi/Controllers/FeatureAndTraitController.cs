using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces;
using webapi.Models.DND;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureAndTraitController : GenericController<FeatureAndTrait>
    {
        public FeatureAndTraitController(IGenericService<FeatureAndTrait> service) : base(service)
        {
        }
    }
}
