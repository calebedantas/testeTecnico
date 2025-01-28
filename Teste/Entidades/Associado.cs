using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Associado
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Nome { get; set; }

    [Required]
    [MaxLength(11)]
    public string Cpf { get; set; }

    public DateTime DataNascimento { get; set; }

    public ICollection<AssociadoEmpresa> AssociadoEmpresas { get; set; }
}