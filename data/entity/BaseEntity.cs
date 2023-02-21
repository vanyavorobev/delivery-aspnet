
namespace hitsLab.data.entity
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsActive { get; set; } = true;
        public string DateCreated { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.ms", System.Globalization.CultureInfo.InvariantCulture);
        public string? DateUpdated { get; set; } = null;
    }
}
