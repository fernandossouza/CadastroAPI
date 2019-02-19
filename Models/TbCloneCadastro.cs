using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroAPI.Models
{
    public class TbCloneCadastro
    {
        public long id{get;set;}
        [Required]
        [MaxLength(20)]
        public string codigo{get;set;}
        [Required]
        [MaxLength(100)]
        public string descricao{get;set;}
        [Required]
        [MaxLength(20)]
        public string cor{get;set;}
        public bool ativo{get;set;}
        public int percentualBifurcada{get;set;}
        public string portaEnxerto{get;set;}
        public string nomeCientifico{get;set;}
        public string nomeComum{get;set;}
        public string categoria{get;set;}
        public  long municipioColetaId{get;set;}
        public  TbCloneMunicipio municipioColeta{get;set;}
        public string criterioSelecao{get;set;}
        public string intensidadeSelecao{get;set;}
        public long municipoTesteId{get;set;}
        public TbCloneMunicipio municipoTeste{get;set;}
        public string codigoCultivar{get;set;}
        public string numeroRegistroCultivar{get;set;}
        public DateTime? dataRegistroCultivar{get;set;}
        public string mantenedoraCultivar{get;set;}
        public long rotaIdCasaVegetacao{get;set;}
        public TbRotasCadastro rotaCasaVegetacao{get;set;}
        public DateTime dataMigracaoSap{get;set;}
        



    }
}