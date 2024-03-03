using AppListaDeCompras.Libraries.Utilities;
using MongoDB.Bson;
using Newtonsoft.Json;
using Realms;

namespace AppListaDeCompras.Models
{
	public partial class User : IRealmObject
	{
		[PrimaryKey]
		[MapTo("_id")]
		[JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }
		
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
