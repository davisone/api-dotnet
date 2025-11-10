using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Service;

namespace WebApplication1.Controller;

[ApiController]
[Route("api/[controller]")]
public class BanqueController : ControllerBase
{
    private readonly IBanqueService _banqueService;
    private readonly ILogger<BanqueController> _logger;

    public BanqueController(IBanqueService banqueService, ILogger<BanqueController> logger)
    {
        _banqueService = banqueService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Banque>>> GetAllBanques()
    {
        try
        {
            var banques = await _banqueService.GetAllBanquesAsync();
            return Ok(banques);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des banques");
            return StatusCode(500, "Une erreur est survenue lors de la récupération des banques");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Banque>> GetBanqueById(int id)
    {
        try
        {
            var banque = await _banqueService.GetBanqueByIdAsync(id);
            if (banque == null)
                return NotFound($"Banque avec l'ID {id} introuvable");

            return Ok(banque);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération de la banque {Id}", id);
            return StatusCode(500, "Une erreur est survenue lors de la récupération de la banque");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Banque>> CreateBanque([FromBody] Banque banque)
    {
        try
        {
            var createdBanque = await _banqueService.CreateBanqueAsync(banque);
            return CreatedAtAction(nameof(GetBanqueById), new { id = createdBanque.Id }, createdBanque);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création de la banque");
            return StatusCode(500, "Une erreur est survenue lors de la création de la banque");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Banque>> UpdateBanque(int id, [FromBody] Banque banque)
    {
        try
        {
            var updatedBanque = await _banqueService.UpdateBanqueAsync(id, banque);
            if (updatedBanque == null)
                return NotFound($"Banque avec l'ID {id} introuvable");

            return Ok(updatedBanque);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la mise à jour de la banque {Id}", id);
            return StatusCode(500, "Une erreur est survenue lors de la mise à jour de la banque");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBanque(int id)
    {
        try
        {
            var result = await _banqueService.DeleteBanqueAsync(id);
            if (!result)
                return NotFound($"Banque avec l'ID {id} introuvable");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la suppression de la banque {Id}", id);
            return StatusCode(500, "Une erreur est survenue lors de la suppression de la banque");
        }
    }
}