using System.Linq;
using System.Security.Claims;

namespace FiiPrezent.Web.Helpers
{
    public static class ClaimsPrincipalHelper
    {
        public static string GetNameIdentifier(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }

        public static string GetName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }

        public static string GetProfileImage(this ClaimsPrincipal claimsPrincipal)
        {
            string identifier = claimsPrincipal.GetNameIdentifier();

            return $"https://graph.facebook.com/{identifier}/picture?type=large";
        }
    }
}