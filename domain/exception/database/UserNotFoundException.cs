
namespace hitsLab.domain.exception.database
{
    public class UserNotFoundException : DbAccessException
    {
        public override int Status { get; set; } = StatusCodes.Status401Unauthorized;
        public override List<string> Errors { get; set; } = new() {"Пользователь не найден" };
    }
}
