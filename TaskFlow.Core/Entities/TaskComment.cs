using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlow.Core.Entities
{
    public class TaskComment : BaseEntity
    {
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public int TaskId { get; set; }

        [ForeignKey(nameof(TaskId))]
        public virtual TaskItem Task { get; set; } = null!;

        [Required]
        public string AuthorId { get; set; } = string.Empty;

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; } = null!;
    }
}