using System;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DotNetOpenAuth.OAuth2;
using Microsoft.AspNet.Identity.EntityFramework;
using BadgesSystem.Web.Models;
using System.Configuration;
using BadgesSystem.Web.Constants;
using System.Threading.Tasks;
using BadgesSystem.Models;
using BadgesSystem.Data;
using System.Web.Security;
using System.Linq;
using System.Data.Entity;

namespace BadgesSystem.Web.Controllers
{
    public class OAuthController : Controller
    {
        private IUowData Data { get; set; }

        public OAuthController(IUowData data)
        {
            this.Data = data;
        }

        public OAuthController()
            : this(new UowData())
        {
        }

        private WebServerClient _webServerClient;
        private readonly string OAuthProvaiderUri = ConfigurationManager.AppSettings["OAuthProvaiderUri"];

        public async Task<ActionResult> LogIn()
        {
            if (!User.Identity.IsAuthenticated)
            {
                InitializeWebServerClient();

                var authorizationState = _webServerClient.ProcessUserAuthorization(Request);

                if (authorizationState != null)
                {
                    var resourceServerUri = new Uri(OAuthProvaiderUri);
                    var client = new HttpClient(_webServerClient.CreateAuthorizingHandler(authorizationState.AccessToken));
                    var body = client.GetStringAsync(new Uri(resourceServerUri, Paths.GetUserPath)).Result;
                    var jss = new JavaScriptSerializer();
                    var user = jss.Deserialize<IdentityUserVM>(body);

                    await this.RegisterOAuthUser(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var userAuthorization = _webServerClient.PrepareRequestUserAuthorization(new[] { "bio", "notes" });
                    userAuthorization.Send(HttpContext);
                    Response.End();
                }
            }

            return null;
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private void InitializeWebServerClient()
        {
            var clientId = ConfigurationManager.AppSettings["ClientId"];
            var clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            var authorizationServerUri = new Uri(OAuthProvaiderUri);
            var authorizationServer = new AuthorizationServerDescription
            {
                AuthorizationEndpoint = new Uri(authorizationServerUri, Paths.AuthorizePath),
                TokenEndpoint = new Uri(authorizationServerUri, Paths.TokenPath)
            };
            _webServerClient = new WebServerClient(authorizationServer, clientId, clientSecret);
        }

        private async Task RegisterOAuthUser(IdentityUserVM identityUser)
        {
            var appUser = await this.Data.Users.All()
                .Where(ur => ur.UserName == identityUser.UserName)
                .FirstOrDefaultAsync();

            if (appUser == null)
            {
                var user = new User()
                {
                    UserName = identityUser.UserName,
                    Email = identityUser.Email,
                    UserRole = UserRole.Programmer,
                    Name = identityUser.FullName
                };

                this.Data.Users.Add(user);
                this.Data.SaveChanges();
                this.SignIn(user, false);
            }
            else
            {
                this.SignIn(appUser, false);
            }
        }

        private void SignIn(User user, bool isPersistent)
        {
            FormsAuthentication.RedirectFromLoginPage(user.UserName, isPersistent);
        }
    }
}