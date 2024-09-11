using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

internal class GameFundTransferUpdateByGameWalletFailedService(
	IGameFundTransferRepository repository) : IRequestHandler<GameFundTransferUpdateByGameWalletFailedRequest>
{
	public Task Handle(GameFundTransferUpdateByGameWalletFailedRequest request, CancellationToken cancellationToken)
	=> repository.UpdateByGameWalletFailedAsync(request.Id, cancellationToken).AsTask();
}
