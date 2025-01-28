public class AssociadoEmpresa
{
    public int AssociadoId { get; set; }
    public Associado Associado { get; set; }

    public int EmpresaId { get; set; }
    public Empresa Empresa { get; set; }
}