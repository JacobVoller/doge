﻿namespace DogeServer.Config.Models;

public class DatabaseConfig
{
    public bool UseInMemoryDatabase { get; set; } = true;
    public string Host { get; set; } = "";
    public string Database { get; set; } = "";
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}
