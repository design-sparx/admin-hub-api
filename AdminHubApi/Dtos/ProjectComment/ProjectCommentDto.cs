﻿using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.ProjectComment;

public class ProjectCommentDto
{
    public int Id { get; set; }
    [Required]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters long.")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(5, ErrorMessage = "Content must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters long.")]
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string CreatedBy { get; set; } = string.Empty;
    public int? ProjectId { get; set; }
}