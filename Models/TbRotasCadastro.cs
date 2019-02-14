using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CadastroAPI.Models
{
    public class TbRotasCadastro
    {
        public long id{get;set;}
        [Required]
        [MaxLength(50)]
        public string nome{get;set;}
        [Required]
        public int prioridade{get;set;}
        [Required]
        public bool ativo{get;set;}
        public ICollection<TbRotasTrechoXRotas> trechoId{get;set;}
        public ICollection<TbRotasTrecho> trechoRota{get;set;}
    }
}