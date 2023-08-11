using TournaBet.Shared.Repository;

namespace TournaBet.Liga.Entities;

public class LigaEntity : BaseEntity
{
    public string Name { get; set; }
    public byte Status { get; set; }
    public byte EntryType { get; set; }
    public decimal EntryFee { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<LigaMatchEntity> Matches { get; set; }
    public List<LigaParticipantEntity> Participants { get; set; }
}