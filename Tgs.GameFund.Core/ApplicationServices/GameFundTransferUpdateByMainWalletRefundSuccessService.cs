using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

internal class GameFundTransferUpdateByMainWalletRefundSuccessService(
	IGameFundTransferRepository repository) : IRequestHandler<GameFundTransferUpdateByMainWalletRefundSuccessRequest>
{
	public Task Handle(GameFundTransferUpdateByMainWalletRefundSuccessRequest request, CancellationToken cancellationToken)
	=> repository.UpdateByMainWalletRefundSuccessAsync(request.Id, request.CashRespTransactionId, request.MainWalletBalance, request.Description, cancellationToken).AsTask();
}
