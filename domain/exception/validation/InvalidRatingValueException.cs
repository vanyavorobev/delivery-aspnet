
namespace hitsLab.domain.exception.validation
{
    public class InvalidRatingValueException : ValidationException
    {
        public override int Status { get; set; } = StatusCodes.Status400BadRequest;
        public override List<string> Errors { get; set; } = new() { "Рейтинг должен находится на промежутке 0 - 10" };
    }
}
