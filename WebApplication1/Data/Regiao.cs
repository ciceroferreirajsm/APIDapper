namespace WebApplication1.Data
{

    public class Regiao
    {
        public int IdRegiao { get; set; }
        public string? CodRegiao { get; set; }
        public string? NomeRegiao { get; set; }
        public List<Estado>? Estados { get; set; }
    }
}
