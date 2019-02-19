using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroAPI.Models
{
    public class TbPessoasCadastro
    {
        public int id { get; set; }
        [Required]
        public string matricula { get; set; }
        [Required]
        [MaxLength(50)]
        public string nome { get; set; }
        [Required]
        public DateTime admissao { get; set; }
        [Required]
        public string cargo { get; set; }
        [Required]
        public DateTime dataNascimento { get; set; }
        [Required]
        public string estadoCivil { get; set; }
        [Required]
        [MaxLength(20)]
        public string sexo { get; set; }
        [Required]
        [MaxLength(15)]
        public string nivelEscolar { get; set; }
        [Required]
        [MaxLength(50)]
        public string escalaTrabalho { get; set; }
        [Required]
        [MaxLength(50)]
        public string municipioOrigem { get; set; }
        [Required]
        [MaxLength(50)]
        public string situacao { get; set; }
        [Required]
        [MaxLength(50)]
        public string tempoEmpresa { get; set; }
        [Required]
        [MaxLength(50)]
        public string crachaRFID { get; set; }
        [Required]
        [MaxLength(50)]
        public string login { get; set; }
        [Required]
        [MaxLength(20)]
        public string password { get; set; }
        [Required]
        public bool ativo { get; set; }
    }
}