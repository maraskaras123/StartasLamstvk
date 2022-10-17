using StartasLamstvk.Shared.Models.RaceOfficial;

namespace StartasLamstvk.Shared.Models.Wage
{
    public class WageReadModel
    {
        public int Id { get; set; }
        public RaceOfficialReadModel RaceOfficial { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public bool IsTransactionDone { get; set; }
    }
}