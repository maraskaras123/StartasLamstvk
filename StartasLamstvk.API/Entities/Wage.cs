namespace StartasLamstvk.API.Entities
{
    public class Wage
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public bool Done { get; set; }
        public int RaceOfficialId { get; set; }

        public virtual RaceOfficial RaceOfficial { get; set; }
    }
}
