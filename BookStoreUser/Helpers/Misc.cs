using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookStoreUser.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookStoreUser.Helpers
{
	public class Misc
	{
		public Misc()
		{
		}

		public static string UploadFile(IFormFile file, IWebHostEnvironment environment)
		{
			var folder = Path.Combine(environment.WebRootPath, "Images");
            string fileExtension = Path.GetExtension(file.FileName);
            var acceptedType = new[] { "jpg", "png", "jpeg", "gif", "JPEG" };
            string fileName = $"{Misc.GenerateReference()}{fileExtension}";

            if (acceptedType.Contains(fileExtension.Substring(1)))
            {
                string storagePath = Path.Combine(folder, fileName);
                FileStream stream = new FileStream(path: storagePath, mode: FileMode.Create);
                file.CopyTo(stream);

                stream.Dispose();

                

                return fileName;
            }
            else
            {
                throw new Exception("Unsupported file type.");
            }
        }

        public static string GenerateReference()
        {
            return Guid.NewGuid().ToString();
        }

        private static string GetTokenFromHeader(IHeaderDictionary requestHeaders)
        {
            if (!requestHeaders.TryGetValue("Authorization", out var authorizationHeader))
                throw new InvalidOperationException("Authorization token does not exists");

            var authorization = authorizationHeader.FirstOrDefault()!.Split(" ");

            var type = authorization[0];

            if (type != "Bearer") throw new InvalidOperationException("You should provide a Bearer token");

            var value = authorization[1] ?? throw new InvalidOperationException("Authorization token does not exists");
            return value;
        }

        public static Task ValidateToken(MessageReceivedContext context)
        {
            try
            {
                context.Token = GetTokenFromHeader(context.Request.Headers);

                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(context.Token, context.Options.TokenValidationParameters, out var validatedToken);

                var jwtSecurityToken = validatedToken as JwtSecurityToken;

                context.Principal = new ClaimsPrincipal();

                var claimsIdentity = new ClaimsIdentity(jwtSecurityToken.Claims.ToList(),
                        "JwtBearerToken", ClaimTypes.NameIdentifier, ClaimTypes.Role);
                context.Principal.AddIdentity(claimsIdentity);

                context.Success();

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                context.Fail(e);
            }

            return Task.CompletedTask;
        }
    }
}

