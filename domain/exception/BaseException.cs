
namespace hitsLab.domain.exception
{
    public abstract class BaseException : Exception
    {
        public abstract int Status { get; set; }
        public abstract List<string> Errors { get; set; }
    }
}