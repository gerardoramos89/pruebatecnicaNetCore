namespace ECommerce.Api.Helpers
{
    public class ReservationDatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string ReservationCollectionName { get; set; } = string.Empty;
    }
}
