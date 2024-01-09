﻿namespace ProfesiNet.Users.Application.Experiences.Dtos;

public class ExperienceDto
{
    public Guid Id { get; set; }
    
    public string? Company { get; set; }
    public string? Position { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid UserId { get; set; }
}