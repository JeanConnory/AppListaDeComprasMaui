using MongoDB.Bson;
using Realms;

namespace AppListaDeCompras.Models
{
	public partial class User : IRealmObject
	{
		[PrimaryKey]
		[MapTo("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
		
		[MapTo("name")]
		public string Name { get; set; }

		[MapTo("email")]
		public string Email { get; set; }

		[MapTo("access_code_temp")]
		public string AccessCodeTemp { get; set; }

		[MapTo("access_code_temp_created_at")]
		public DateTimeOffset AccessCodeTempCreatedAt { get; set; }

		[MapTo("created_at")]
		public DateTimeOffset CreatedAt { get; set; }
	}
}
