namespace Web_Management_API.DisplayRowModels
{
    public class ExtendedInfoProduct
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }

        public int TotalAmount { get; set; }

        public int ReservedAmount { get; set; }

        public int StorageAmount { get; set; }
    }
}
