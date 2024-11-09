using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.Project;

public class UpdateProjectRequestDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Name must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Name cannot be over 280 characters long.")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MinLength(5, ErrorMessage = "Description must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Description cannot be over 280 characters long.")]
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}