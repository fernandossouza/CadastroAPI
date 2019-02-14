using System.Collections.Generic;

namespace CadastroAPI.Models
{
    public class TbRotasSentido
    {
        public long id{get;set;}
        public string sentido {get;set;}
        public bool ativo{get;set;}
        public ICollection<TbRotasProximoTrecho> proximoTrecho{get;set;}
        
    }
}