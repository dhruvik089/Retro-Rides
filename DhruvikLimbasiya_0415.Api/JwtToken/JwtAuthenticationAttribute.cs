using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace DhruvikLimbasiya_0415.Api.JwtToken
{
    public class JwtAuthenticationAttribute : FilterAttribute

    {

        public static void OnAuthorization(HttpContext context)

        {

            var request = context.Request.Headers;

            var token = request["Authorization"];

            if (string.IsNullOrEmpty(token))

            {

                throw new UnauthorizedAccessException("Token is missing.");

            }
             

            if (token.StartsWith("Bearer "))

            {

                token = token.Substring("Bearer ".Length).Trim();

            }

            if (string.IsNullOrEmpty(token))

            {

                throw new UnauthorizedAccessException("Token is null.");

            }

            var userName = Authentication.ValidateToken(token);

            if (userName == null)

            {

                throw new UnauthorizedAccessException("Invalid token.");

            }

        }
    }
}