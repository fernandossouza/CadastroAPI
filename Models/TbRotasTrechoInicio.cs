namespace CadastroAPI.Models
{
    public class TbRotasTrechoInicio
    {
        public long id{get;set;}
        public long trechoId{get;set;}
        public string inicio{get;set;}
        public TbRotasTrecho trecho{get;set;}
    }
}