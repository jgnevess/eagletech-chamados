using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using eagletechapi.dto.usuario;
using eagletechapi.entity.usuario;
using Microsoft.EntityFrameworkCore;

namespace eagletechapi.models.usuario
{
    [Index(nameof(Username), IsUnique = true)]
    public class Usuario
    {
        [Key]
        public int Matricula { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 40 caracteres")]
        [RegularExpression("^[A-Za-zÀ-ÖØ-öø-ÿ]+(?: [A-Za-zÀ-ÖØ-öø-ÿ]+)*$", ErrorMessage = "O nome só pode conter letras")]
        public string NomeCompleto { get; set; }

        [Required]
        [JsonIgnore]
        public string Senha { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O telefone deve ter 11 caracteres")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "O telefone só pode conter números")]
        public string Telefone { get; set; }
        [Required]
        public Funcao Funcao { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "O username não é valido")]
        public string Username { get; set; }
        public bool FirstLogin { get; set; } = true;

        public bool Ativo { get; set; } = true;
        public Theme Theme { get; set; }

        public Usuario()
        {
            this.Matricula = 0;
            this.NomeCompleto = string.Empty;
            this.Senha = string.Empty;
            this.Telefone = string.Empty;
            this.Funcao = Funcao.SOLICITANTE;
            this.Username = string.Empty;
            this.Theme = Theme.light;
        }

        public Usuario(UserIn userIn, string senha)
        {
            this.NomeCompleto = userIn.NomeCompleto;
            this.Senha = BCrypt.Net.BCrypt.HashPassword(senha);
            this.Telefone = userIn.Telefone;
            this.Funcao = userIn.Funcao;
            this.Username = userIn.Username;
            this.Theme = Theme.light;
        }

        public Usuario(UserIn userIn)
        {
            this.NomeCompleto = userIn.NomeCompleto;
            this.Senha = BCrypt.Net.BCrypt.HashPassword(userIn.Senha);
            this.Telefone = userIn.Telefone;
            this.Funcao = userIn.Funcao;
            this.Username = userIn.Username;
            this.Theme = Theme.light;
        }
    }
}