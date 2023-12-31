﻿namespace ProfesiNet.Posts.Core.Commands.Create;

internal record CreateCommentCommand(string? Content, Guid PostId)
{
    public Guid Id { get; init; }
}