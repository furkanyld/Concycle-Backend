using Concycle.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Concycle.Business.Interfaces;

namespace Concycle.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionDto>>> GetAllTranactions()
        {
            var list = await _transactionService.GetAllTransactions();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransactionById(Guid id)
        {
            var transaction = await _transactionService.GetTransactionById(id);
            if (transaction is null) return NotFound();
            return Ok(transaction);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<TransactionDto>>> GetTransactionsByUser(Guid userId)
        {
            var transactions = await _transactionService.GetTransactionsByUser(userId);
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> CreateTransaction([FromBody] TransactionDto dto)
        {
            var created = await _transactionService.CreateTransaction(dto);

            if (created is null)
                return BadRequest("Katkı puanınız bu işlem için yetersiz.");

            return CreatedAtAction(nameof(GetTransactionById), new { id = created.Id }, created);
        }

    }
}
