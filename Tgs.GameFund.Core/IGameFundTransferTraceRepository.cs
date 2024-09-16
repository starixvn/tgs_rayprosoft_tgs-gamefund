namespace Tgs.GameFund.Core;

public interface IGameFundTransferTraceRepository
{
	ValueTask SaveTraceAsync(Guid gameProviderId, Guid walletId, string playerCode, CancellationToken cancellationToken = default);

	IAsyncEnumerable<string> QueryPlayerCode(string gameProviderCode, string brandCode, DateTimeOffset? startTime, DateTimeOffset? endTime, CancellationToken cancellationToken = default);

	IAsyncEnumerable<string> QueryPlayerCode(string gameProviderCode, DateTimeOffset? startTime, DateTimeOffset? endTime, CancellationToken cancellationToken = default);
}
