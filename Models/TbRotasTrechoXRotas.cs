using System.ComponentModel.DataAnnotations;

namespace CadastroAPI.Models
{
    public class TbRotasTrechoXRotas
    {
        public long id{get;set;}
        [Required]
        public long sentidoId{get;set;}
        [Required]
        public int ordem{get;set;}
        public long rotaId{get;set;}
    }
}