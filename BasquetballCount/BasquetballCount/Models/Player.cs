using SQLite;

namespace BasquetballCount.Models
{
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamId { get; set; }
        public int Number { get; set; }
        public int Points { get; set; }
        public int Rebounds { get; set; }
        public int Asist { get; set; }
    }
}
