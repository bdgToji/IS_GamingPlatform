using System.ComponentModel.DataAnnotations;

namespace GamePlatform.Domain.Domain
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
