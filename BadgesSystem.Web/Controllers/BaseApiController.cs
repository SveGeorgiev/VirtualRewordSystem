using BadgesSystem.Data;
using BadgesSystem.Web.Infrastructure;
using System.Web.Http;

namespace BadgesSystem.Web.Controllers
{
    [CustomAuthorize]
    public class BaseApiController : ApiController
    {
        protected IUowData Data { get; set; }


        public BaseApiController(IUowData data)
        {
            this.Data = data;
        }

        public BaseApiController()
            : this(new UowData())
        {
        }
    }
}
