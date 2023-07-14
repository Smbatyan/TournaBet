namespace TournaBet.Liga.Entities;

public class LigaMatchEntity
{
    public int Id { get; set; }
    public int LigaId { get; set; }
    public int MatchId { get; set; }
    public int StakeId { get; set; }
    public byte? ActualResult { get; set; }
}