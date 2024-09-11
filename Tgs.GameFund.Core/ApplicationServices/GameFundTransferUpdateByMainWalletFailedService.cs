using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

internal class GameFundTransferUpdateByMainWalletFailedService(
	IGameFundTransferRepository repository) : IRequestHandler<GameFundTransferUpdateByMainWalletFailedRequest>
{
	public Task Handle(GameFundTransferUpdateByMainWalletFailedRequest request, CancellationToken cancellationToken)
	=> repository.UpdateByMainWalletFailedAsync(request.Id, cancellationToken).AsTask();
}
