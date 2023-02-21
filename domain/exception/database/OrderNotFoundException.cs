
namespace hitsLab.domain.exception.database
{
    public class OrderNotFoundException : DbAccessException
    {
        public override int Status { get; set; } = StatusCodes.Status404NotFound;
        public override List<string> Errors { get; set; } = new() {"Заказ не найден" };
    }
}

