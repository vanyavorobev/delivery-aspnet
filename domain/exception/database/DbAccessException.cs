
namespace hitsLab.domain.exception.database
{
    public class DbAccessException: BaseException
    {
        public override int Status { get; set; } = StatusCodes.Status404NotFound;
        public override List<string> Errors { get; set; } = new() { "Ошибка в работе с базой данных" };
    }    
}
