using Microsoft.Extensions.Configuration;

namespace CadastroAPI.Models.Repository
{
    public class AbstractRepository
    {
        private string stringConnection;
        private readonly IConfiguration _configuration;
        public AbstractRepository(IConfiguration configuration)
        {
            stringConnection = _configuration.GetConnectionString("CadastroConnection");
        }
    }
}