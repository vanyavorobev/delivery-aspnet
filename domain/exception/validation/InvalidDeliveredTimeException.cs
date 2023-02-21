
namespace hitsLab.domain.exception.validation
{
    public class InvalidDeliveredTimeException : ValidationException
    {
        public override int Status { get; set; } = StatusCodes.Status400BadRequest;
        public override List<string> Errors { get; set; } = new() { "Заказ не может быть доставлен в такое время" };
    }
}

