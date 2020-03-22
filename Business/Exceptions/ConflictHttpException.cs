using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;

namespace WeVsVirus.Business.Exceptions
{
    public class ConflictHttpException : HttpStatusCodeException
    {

        public ConflictHttpException(string message) : base(StatusCodes.Status403Forbidden, new JObject { { "error", message } })
        {
        }

        public ConflictHttpException()
           : base(StatusCodes.Status409Conflict, new JObject { { "error", "Die angeforderte Ressource steht dir nicht zur verfügung." } })
        {
        }

        public ConflictHttpException(Exception innerException) : base(StatusCodes.Status403Forbidden, innerException)
        {
        }
    }
}
