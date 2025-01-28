using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Empresa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Nome { get; set; }

    [Required]
    [MaxLength(14)]
    public string Cnpj { get; set; }

    public ICollection<AssociadoEmpresa> AssociadoEmpresas { get; set; }
}