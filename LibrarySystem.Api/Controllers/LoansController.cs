using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpGet]
    public async Task<ActionResult<List<LoanDto>>> GetAll(CancellationToken cancellationToken)
    {
        var loans = await _loanService.GetAllAsync(cancellationToken);
        return Ok(loans);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LoanDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var loan = await _loanService.GetByIdAsync(id, cancellationToken);
        if (loan == null) return NotFound();
        return Ok(loan);
    }

    [HttpGet("member/{memberId}")]
    public async Task<ActionResult<List<LoanDto>>> GetActiveByMember(
        Guid memberId,
        CancellationToken cancellationToken)
    {
        var loans = await _loanService.GetActiveLoansByMemberIdAsync(memberId, cancellationToken);
        return Ok(loans);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateLoanRequest request,
        CancellationToken cancellationToken)
    {
        var id = await _loanService.CreateAsync(
            request.BookId,
            request.MemberId,
            cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPost("{id}/return")]
    public async Task<IActionResult> Return(Guid id, CancellationToken cancellationToken)
    {
        await _loanService.ReturnAsync(id, cancellationToken);
        return NoContent();
    }
}

public record CreateLoanRequest(Guid BookId, Guid MemberId);