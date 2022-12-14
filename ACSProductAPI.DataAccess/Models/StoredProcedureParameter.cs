namespace DataAccess.Models
{
    public class StoredProcedureParameter
    {
        public StoredProcedureParameter()
        {
            Type = NpgsqlTypes.NpgsqlDbType.Varchar;
        }
        public string? Name { get; set; }

        public NpgsqlTypes.NpgsqlDbType Type { get; set; }

        public object? Value { get; set; }
    }
}
