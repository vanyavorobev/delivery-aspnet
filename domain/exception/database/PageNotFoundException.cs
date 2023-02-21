
namespace hitsLab.domain.exception.database
{
    public class PageNotFoundException : DbAccessException
    {
        public override int Status { get; set; } = StatusCodes.Status404NotFound;
        public override List<string> Errors { get; set; } = new() {"Страница не найдена" };
    } 
}
