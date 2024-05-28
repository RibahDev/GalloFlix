using System.ComponentModel.DataAnnotations;

namespace RibahFlix.ViewModels;
    public class RegisterVM
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Por favor, informe seu nome")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Por favor, informe seu email")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Por favor, informe sua data de nascimento")]
        public sbyte Birthday { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha de Acesso")]
        [Required(ErrorMessage = "Por favor, informe seu email ou nome de usuário")]
        public string Password { get; set; }

        
    }