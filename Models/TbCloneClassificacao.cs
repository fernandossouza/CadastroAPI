using System.Collections.Generic;

namespace CadastroAPI.Models
{
    public class TbCloneClassificacao
    {
        public long id{get;set;}
        public string classificacao{get;set;}
        public TbCloneClassificacaoXClone classificacaoPorClone{get;set;}

    }
}