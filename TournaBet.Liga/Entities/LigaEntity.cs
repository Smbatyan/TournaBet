namespace TournaBet.Liga.Entities;

public class LigaEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte Status { get; set; }
    public decimal EntryFee { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<LigaMatchEntity> Matches { get; set; }
    public List<ParticipantEntity> Participants { get; set; }
}