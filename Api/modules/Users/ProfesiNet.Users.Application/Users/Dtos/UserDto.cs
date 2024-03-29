﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProfesiNet.Users.Application.Photos.Dtos;

namespace ProfesiNet.Users.Application.Users.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public bool Following { get; set; }
    public bool FollowedBy { get; set; }
    
    public string? Address { get; set; }
    public string? Bio { get; set; }
    public string? Title { get; set; }
    public string? ProfilePicture{ get; set; }
}

public class UserDetailsDto : UserDto
{
    public int FollowingsCount { get; set; }
    public int FollowersCount { get; set; }
    public int NetworkConnectionsCount { get; set; }
}