
namespace hitsLab.domain.exception.auth
{
    public class AuthorizationException : BaseException
    {
        public override int Status { get; set; } = StatusCodes.Status401Unauthorized;
        public override List<string> Errors { get; set; } = new() {" Unauthorized "};
    }
}
