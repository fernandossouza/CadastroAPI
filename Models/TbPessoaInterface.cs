using System;

namespace CadastroAPI.Models
{
    public class TbPessoaInterface
    {
        public long id{get;set;}
        public string rfid{get;set;}
        public string mesa{get;set;}
        public DateTime? dtLido{get;set;}
    }
}