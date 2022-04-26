namespace SpellIt.Authen.Service;

public record UserInfo(string username, string name, string city);
public record AuthenticationRequestBody(string username, string password);

public interface IAuthenticationService1
{
    UserInfo ValidateUser(string username, string password);
}

public interface IAuthenticationService
{
    UserInfo ValidateUser(string username, string password);
}

public class AuthenticationService : IAuthenticationService
{
    public UserInfo ValidateUser(string username, string password)
    {
        return new UserInfo("jarav", "Verbrugghe Jara", "Tielt");
    }
}