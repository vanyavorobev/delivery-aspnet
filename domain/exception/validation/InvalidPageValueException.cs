
namespace hitsLab.domain.exception.validation
{
    public class InvalidPageValueException : ValidationException
    {
        public override int Status { get; set; } = StatusCodes.Status400BadRequest;
        public override List<string> Errors { get; set; } = new() { "Значение страницы должно быть больше 0" };
    }    
}
