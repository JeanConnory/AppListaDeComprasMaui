using MongoDB.Bson;
using Realms;

namespace AppListaDeCompras.Models
{
	public partial class ListToBuy : IRealmObject
	{
		[PrimaryKey]
		[MapTo("_id")]
		public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

		[MapTo("name")]
		public string Name { get; set; }

		[MapTo("products")]
		public IList<Product> Products { get; }

		[MapTo("users")]
		public IList<User> Users { get; }

		[MapTo("created_at")]
		public DateTimeOffset CreatedAt { get; set; }
    }
}
