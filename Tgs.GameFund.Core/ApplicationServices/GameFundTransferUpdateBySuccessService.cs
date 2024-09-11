using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

internal class GameFundTransferUpdateBySuccessService(
	IGameFundTransferRepository repository) : IRequestHandler<GameFundTransferUpdateBySuccessRequest>
{
	public Task Handle(GameFundTransferUpdateBySuccessRequest request, CancellationToken cancellationToken)
	=> repository.UpdateBySuccessAsync(request.Id, request.CashRespTransactionId, request.MainWalletBalance, cancellationToken).AsTask();
}
