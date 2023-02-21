
namespace hitsLab.domain.exception.validation
{
    public class InvalidTokenTypeException : ValidationException
    {
        public override int Status { get; set; } = StatusCodes.Status400BadRequest;
        public override List<string> Errors { get; set; } = new() {"Неправильный тип токена" };
    }
}