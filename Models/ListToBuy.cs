namespace AppListaDeCompras.Models
{
	public class ListToBuy
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public List<Product> Products { get; set; }
        public List<User> Users { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
