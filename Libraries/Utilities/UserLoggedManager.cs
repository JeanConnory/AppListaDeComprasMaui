using AppListaDeCompras.Models;
using Newtonsoft.Json;

namespace AppListaDeCompras.Libraries.Utilities
{
	public class UserLoggedManager
	{
		private static string _key = "storage.user";

		public static User GetUser()
		{
			var userAsString = Preferences.Get(_key, null);
			if (userAsString != null)
			{
				return JsonConvert.DeserializeObject<User>(userAsString);
			}
			return null;
		}

		public static void SetUSer(User user)
		{
			RemoveUser();

			var userToJsonSerializer = new { user.Id, user.Name, user.Email, user.CreatedAt };

			Preferences.Set(_key, JsonConvert.SerializeObject(userToJsonSerializer));
		}

		public static void RemoveUser()
		{
			if(ExistsUser())
				Preferences.Remove(_key);
		}

		public static bool ExistsUser()
		{
			return Preferences.ContainsKey(_key);
		}
	}
}
