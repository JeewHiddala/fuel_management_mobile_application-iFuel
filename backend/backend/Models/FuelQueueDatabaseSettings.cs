namespace Ifuel.Models;

public class FuelQueueDatabaseSettings {
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string FuelQueueName { get; set; } = null!;
}