using System.Collections.Generic;

namespace CadastroAPI.Models
{
    public class TbRotasTrecho
    {
        public long id{get;set;}
        public string trecho{get;set;}
        public bool ativo{get;set;}
        public ICollection<TbRotasSentido> direcao{get;set;}
        public ICollection<TbRotasTrechoFinal> final{get;set;}
        
    }
}