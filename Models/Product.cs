using MongoDB.Bson;
using Realms;

namespace AppListaDeCompras.Models
{
	public partial class Product : IRealmObject
	{
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

		[MapTo("name")]
		public string Name { get; set; }

		[MapTo("quantity")]
		public decimal Quantity { get; set; }

		[MapTo("quantity_unit_measure")]
		public int QuantityUnitMeasure { get; set; }

		[MapTo("price")]
		public decimal Price { get; set; }

		[MapTo("created_at")]
		public DateTimeOffset CreatedAt { get; set; }

		public bool hasCaught = false;

		[MapTo("has_caught")]
		public bool HasCaught
        {
            get
            {
                return hasCaught;
            }
            set
            {
                hasCaught = value;
                OnPropertyChanged(nameof(HasCaught));
            }
        }
    }
}
