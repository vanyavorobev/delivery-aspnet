
namespace hitsLab.domain.exception.forbidden
{
    public class ForbiddenToSetRatingException : ForbiddenResourceException
    {
        public override int Status { get; set; } = StatusCodes.Status400BadRequest;
        public override List<string> Errors { get; set; } = new() { "Можно поставить рейтинг только на блюда, которые были куплены" };
    }
}