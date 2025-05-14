using AutoMapper;
using Concycle.Business.Interfaces;
using Concycle.Core.Dtos;
using Concycle.Core.Entities;
using Concycle.Data.Interfaces;
using Concycle.Data.Repositories;

namespace Concycle.Business.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public TransactionService(ITransactionRepository repository, IMapper mapper, IUserRepository userRepository, IPostRepository postRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _userRepository = userRepository;
        _postRepository = postRepository;
    }

    public async Task<List<TransactionDto>> GetAllTransactions()
    {
        var transactions = await _repository.GetAllAsync();
        return _mapper.Map<List<TransactionDto>>(transactions);
    }

    public async Task<TransactionDto?> GetTransactionById(Guid id)
    {
        var transaction = await _repository.GetByIdAsync(id);
        return transaction is null ? null : _mapper.Map<TransactionDto>(transaction);
    }

    public async Task<List<TransactionDto>> GetTransactionsByUser(Guid userId)
    {
        var list = await _repository.GetByUserAsync(userId);
        return _mapper.Map<List<TransactionDto>>(list);
    }

    public async Task<TransactionDto> CreateTransaction(TransactionDto dto)
    {
        var transaction = _mapper.Map<Transaction>(dto);
        transaction.Id = Guid.NewGuid();
        transaction.Timestamp = DateTime.UtcNow;

        var fromUser = await _userRepository.GetUserByIdAsync(transaction.FromUserId);
        var toUser = await _userRepository.GetUserByIdAsync(transaction.ToUserId);
        var post = await _postRepository.GetPostByIdAsync(transaction.PostId);

        if (fromUser is null || toUser is null || post is null)
            throw new Exception("Gerekli kullanıcı veya post bulunamadı");

        var payer = post.Type.ToLower() == "help" ? toUser : fromUser;

        payer.Score -= transaction.Score;
        var receiver = (payer == fromUser) ? toUser : fromUser;
        receiver.Score += transaction.Score;

        await _repository.AddAsync(transaction);
        await _repository.SaveAsync();
        await _userRepository.SaveUserAsync();

        return _mapper.Map<TransactionDto>(transaction);
    }
}
