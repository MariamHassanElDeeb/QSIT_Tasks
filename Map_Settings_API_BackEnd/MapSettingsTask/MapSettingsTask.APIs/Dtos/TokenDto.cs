﻿namespace MapSettingsTask.APIs.Dtos;

public class TokenDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiry { get; set; }
}