
namespace hitsLab.domain.exception.auth
{
    public class LoginFailedException : AuthorizationException
    {
        public override int Status { get; set; } = StatusCodes.Status401Unauthorized;
        public override List<string> Errors { get; set; } = new() { "Ошибка входа" };
    }
}

