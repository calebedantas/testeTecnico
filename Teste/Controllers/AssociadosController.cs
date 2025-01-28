using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AssociadosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AssociadosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAssociado([FromBody] Associado associado, [FromQuery] List<int> empresasIds)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        associado.AssociadoEmpresas = empresasIds.Select(id => new AssociadoEmpresa { EmpresaId = id }).ToList();

        _context.Associados.Add(associado);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAssociadoPorId), new { id = associado.Id }, associado);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAssociadoPorId(int id)
    {
        var associado = await _context.Associados
            .Include(a => a.AssociadoEmpresas)
            .ThenInclude(ae => ae.Empresa)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (associado == null) return NotFound();
        return Ok(associado);
    }

    [HttpGet]
    public async Task<IActionResult> ListarAssociados([FromQuery] string? nome)
    {
        var query = _context.Associados.AsQueryable();

        if (!string.IsNullOrEmpty(nome))
            query = query.Where(a => a.Nome.Contains(nome));

        var associados = await query.ToListAsync();
        return Ok(associados);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarAssociado(int id, [FromBody] Associado associado, [FromQuery] List<int> empresasIds)
    {
        var associadoDb = await _context.Associados
            .Include(a => a.AssociadoEmpresas)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (associadoDb == null) return NotFound();

        associadoDb.Nome = associado.Nome;
        associadoDb.Cpf = associado.Cpf;
        associadoDb.DataNascimento = associado.DataNascimento;

        associadoDb.AssociadoEmpresas = empresasIds.Select(eId => new AssociadoEmpresa { AssociadoId = id, EmpresaId = eId }).ToList();

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirAssociado(int id)
    {
        var associado = await _context.Associados.FindAsync(id);
        if (associado == null) return NotFound();

        _context.Associados.Remove(associado);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
