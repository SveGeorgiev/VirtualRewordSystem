using Microsoft.AspNet.Identity.EntityFramework;

namespace BadgesSystem.Web.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Action { get; set; }
        public IdentityUser ApiResponse { get; set; }
    }
}