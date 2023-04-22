namespace TexoIT.Teste.Domain.DataTransfer;

public class PremiumProceducerDTO
{
    public string Producer { get; set; }
    public  int Interval { get; set; }
    public  int PreviusWin { get; set; }
    public  int FollowingWin { get; set; }
}

public class PremiumProceducerReponse
{
    public List<PremiumProceducerDTO> min { get; set; }
    public List<PremiumProceducerDTO> max { get; set; }
}