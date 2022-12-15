namespace DataAccess.Models
{
    public class DatabaseConnections
    {
        public Connection Default { get; set; }
        public Connection SMS { get; set; }
        public Connection CaaS { get; set; }
    }

    public class Connection
    {
        public string Schema { get; set; }
        public string ConnectionString { get; set; }
    }
}
