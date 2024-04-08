using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RibahFlix.Models;
// Declarar nome do banco
[Table("Movie")]
public class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public sbyte Id { get; set; }

    [Display(Name = "Título Original")]
    [Required(ErrorMessage ="Por favor, informe o Título Original")]
    [StringLength(100, ErrorMessage = "O nome deve possuir no máximo 100  caracteres")]
    public string Name { get; set; }
}