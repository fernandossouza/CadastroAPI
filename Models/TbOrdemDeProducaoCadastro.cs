namespace CadastroAPI.Models
{
    public class TbOrdemDeProducaoCadastro
    {
        public long id { get; set; }
        public string op { get; set; }
        public int qntMudas { get; set; }
        public int qntLotes { get; set; }
        public int qntProduzida { get; set; }
        public int qntPerdida { get; set; }
        public long idClone { get; set; }
        public string clone{get;set;}
    }
}