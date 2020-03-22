using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeVsVirus.Business.Utility
{
    public class JwtTokens
    {
        public static async Task<object> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var id = identity.Claims.Single(c => c.Type == Constants.Strings.JwtClaimIdentifiers.Id).Value;
            var typeClaim = identity.Claims.FirstOrDefault(c => c.Type == Constants.Strings.JwtClaimIdentifiers.AccountType);
            if (typeClaim != null)
            {
                var response = new
                {
                    //id = identity.Claims.Single(c => c.Type == "id").Value,
                    auth_token = await jwtFactory.GenerateEncodedToken(id, userName, identity),
                    expires_in = (int)jwtOptions.ValidFor.TotalSeconds,
                    account_type = typeClaim.Value
                };
                // return JsonConvert.SerializeObject(response, serializerSettings);
                return response;
            }
            else{
                var response = new
                {
                    id = identity.Claims.Single(c => c.Type == "id").Value,
                    auth_token = await jwtFactory.GenerateEncodedToken(id, userName, identity),
                    expires_in = (int)jwtOptions.ValidFor.TotalSeconds
                };
                return response;
                // return JsonConvert.SerializeObject(response, serializerSettings);
            }
        }
    }
}