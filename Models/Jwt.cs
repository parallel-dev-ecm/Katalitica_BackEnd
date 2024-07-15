using System.Security.Claims;
namespace Katalitica_API.Models
{
	public class Jwt
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string Subject { get; set; }




		public static dynamic ValidateToken(ClaimsIdentity identity)
		{
			try
			{

				if (identity.Claims.Count() == 0)
				{
					return new
					{
						success = false,
						message = "Error. Check valid access token",
						result = ""
					};
				}

				var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;

                UserModel _user = UserModel.testDB().FirstOrDefault(x => x.userId == id);
                return new
				{
					success = true,
					message = "Success!",
					result = _user
				};


			}
			catch (Exception ex)
			{
				return new
				{
					success = false,
					message = "Catch: " + ex.Message,
					result = ""
				};
			}

		}
	}

}
