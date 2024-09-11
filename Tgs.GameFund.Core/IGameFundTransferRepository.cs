namespace Tgs.GameFund.Core;

public interface IGameFundTransferRepository
{
	IAsyncEnumerable<Models.GameFundTransferInfo> QueryPlayerAsync(string playerCode, DateTimeOffset startTime, DateTimeOffset endTime, Common.Enums.GameFundTransferSort sort, bool isAscending = true, CancellationToken cancellationToken = default);

	ValueTask<Models.GameFundTransferInfo?> GetAsync(Guid id, CancellationToken cancellationToken = default);

	ValueTask AddAsync(Models.GameFundTransferAdd source, CancellationToken cancellationToken = default);

	ValueTask UpdateByGameWalletFailedAsync(Guid id, CancellationToken cancellationToken = default);

	ValueTask UpdateByMainWalletFailedAsync(Guid id, CancellationToken cancellationToken = default);

	ValueTask UpdateByMainWalletRefundFailedAsync(Guid id, string description, CancellationToken cancellationToken = default);

	ValueTask UpdateByMainWalletRefundSuccessAsync(Guid id, Guid cashRespTransactionId, decimal balance, string description, CancellationToken cancellationToken = default);

	ValueTask UpdateBySuccessAsync(Guid id, Guid cashRespTransactionId, decimal balance, CancellationToken cancellationToken = default);

	ValueTask SaveTraceAsync(Guid gameProviderId, Guid walletId, string playerCode, CancellationToken cancellationToken = default);
}
