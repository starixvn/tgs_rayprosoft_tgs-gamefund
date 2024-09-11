using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

internal class GameFundTransferUpdateByMainWalletRefundFailedService(
	IGameFundTransferRepository repository) : IRequestHandler<GameFundTransferUpdateByMainWalletRefundFailedRequest>
{
	public Task Handle(GameFundTransferUpdateByMainWalletRefundFailedRequest request, CancellationToken cancellationToken)
	=> repository.UpdateByMainWalletRefundFailedAsync(request.Id, request.Description, cancellationToken).AsTask();
}
