using System;
using System.Linq;
using System.Security.Cryptography;
using Database.Context;
using Microsoft.EntityFrameworkCore;
using Business; // For Result class
using Database;
public class TokenService
{
    EventContext context = new EventContext();
    public string GenerateToken(int size = 32)
    {
        var bytes = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes)
            .Replace("+", "")
            .Replace("/", "")
            .Replace("=", "");
    }

    // Create and save token for the user email, expires in 30 minutes
    public Result CreatePasswordResetToken(string email)
    {
        try
        {
            var token = GenerateToken();
            var now = DateTime.UtcNow;

            var resetToken = new Token
            {
                Email = email,
                RandomToken = token,
                CreatedAt = now,
                ExpiresAt = now.AddMinutes(30),
                IsUsed = false
            };
            context.Token.Add(resetToken);
            return new Result().DBcommit(context, "Token Generated Successfully", null, resetToken);
        }
        catch (Exception ex)
        {
            return new Result(false, $"Error generating token: {ex.Message}");
        }
    }

    // Validate token (exists, not expired, not used)
    public Result ValidateToken(string email, string token)
    {
        var record = context.Token
            .Where(t => t.Email == email && t.RandomToken == token && !t.IsUsed)
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefault();

        if (record == null)
            return new Result(false, "Invalid or expired token.");

        if (DateTime.UtcNow > record.ExpiresAt)
            return new Result(false, "Token has expired.");

        return new Result(true, "Token is valid.", record);
    }

    // Mark token as used after successful reset
    public Result MarkTokenAsUsed(string email, string token)
    {
        Token? record = context.Token
            .FirstOrDefault(t => t.Email == email && t.RandomToken == token && !t.IsUsed);

        if (record == null)
            return new Result(false, "Token not found or already used.");

        try
        {
            record.IsUsed = true;
            context.Token.Update(record);
            return new Result().DBcommit(context, "User info updated successfully", null, record);
        }
        catch (Exception ex)
        {
            return new Result(false, $"Error marking token as used: {ex.Message}");
        }
    }
}
