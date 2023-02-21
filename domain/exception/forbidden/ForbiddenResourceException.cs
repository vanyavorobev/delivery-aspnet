
namespace hitsLab.domain.exception.forbidden
{
    public class ForbiddenResourceException : BaseException
    {
        public override int Status { get; set; } = StatusCodes.Status403Forbidden;
        public override List<string> Errors { get; set; } = new() { "Доступ к этому ресурсу был запрещен" };
    }
}
