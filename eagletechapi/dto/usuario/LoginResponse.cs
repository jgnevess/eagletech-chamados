using eagletechapi.models.usuario;

namespace eagletechapi.dto.usuario;

public class LoginResponse(string token, string role, int matricula, UserOut userOut, bool firstLogin)
{
    public string Token { get; set; } = token;
    public string Role { get; set; } = role;
    public int Matricula { get; set; } = matricula;
    public UserOut User { get; set; } = userOut;
    public bool FirstLogin { get; set; } = firstLogin;
}