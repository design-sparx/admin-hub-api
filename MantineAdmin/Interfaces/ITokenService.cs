namespace MantineAdmin.Interfaces;

public interface ITokenService
{
    string GenerateToken(AppUser user);
}