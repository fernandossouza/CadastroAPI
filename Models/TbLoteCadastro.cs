namespace CadastroAPI.Models
{
    public class TbLoteCadastro
    {
        public long Id {get; set;}
        public long ordemProducaoId {get; set;}
        public int qntMuda {get; set;}
        public int qntPerdida {get; set;}
        public bool status {get; set;}
        public int semana {get; set;}
        public string lote {get; set;}
        public string ordemProducao{get;set;}
        public string clone{get;set;}
    }
}