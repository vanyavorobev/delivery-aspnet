
using System.ComponentModel.DataAnnotations.Schema;

namespace hitsLab.data.entity
{
    [Table("token")]
    public class TokenEntity : BaseEntity
    {
        public string Token { get; set; }
        public string Type { get; set; }

        public TokenEntity(string token, string type)
        {
            Token = token;
            Type = type;
        }
    }
    
    public enum TokenType
    {
        Access,
        Refresh
    }
}

