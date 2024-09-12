namespace Tgs.GameFund.Core;

public interface IGameFundTransferTraceRepository
{
	ValueTask SaveTraceAsync(Guid gameProviderId, Guid walletId, string playerCode, CancellationToken cancellationToken = default);
}
