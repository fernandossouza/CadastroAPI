using System.Collections.Generic;

namespace CadastroAPI.Models
{
    public class TbCloneSelecao
    {
        public long id{get;set;}
        public string selecao{get;set;}
        public ICollection<TbCloneClassificacao> classificacao{get;set;}
    }
}