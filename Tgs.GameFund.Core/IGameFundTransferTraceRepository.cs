namespace Tgs.GameFund.Core;

public interface IGameFundTransferTraceRepository
{
	ValueTask SaveTraceAsync(Guid gameProviderId, Guid walletId, string playerCode, CancellationToken cancellationToken = default);

	IAsyncEnumerable<string> QueryPlayerCode(string gameProviderCode, string brandCode, CancellationToken cancellationToken = default);

	IAsyncEnumerable<string> QueryPlayerCode(string gameProviderCode, int year, int month, CancellationToken cancellationToken = default);
}
