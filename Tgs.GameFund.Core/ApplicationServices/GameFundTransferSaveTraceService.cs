using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

internal class GameFundTransferSaveTraceService(
	IGameFundTransferTraceRepository repository) : IRequestHandler<GameFundTransferSaveTraceRequest>
{
	public Task Handle(GameFundTransferSaveTraceRequest request, CancellationToken cancellationToken)
        => repository
            .SaveTraceAsync(
                gameProviderId: request.GameProviderId,
                walletId: request.WalletId,
                playerCode: request.PlayerCode,
                cancellationToken: cancellationToken)
            .AsTask();
}
