namespace StartasLamstvk.Shared.Models.RaceOfficial
{
    public class RaceOfficialWriteModel
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string ArrivalTime { get; set; }
    }
}