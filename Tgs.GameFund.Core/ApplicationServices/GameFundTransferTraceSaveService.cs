using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

internal class GameFundTransferTraceSaveService(
	IGameFundTransferTraceRepository repository) : IRequestHandler<GameFundTransferTraceSaveRequest>
{
	public Task Handle(GameFundTransferTraceSaveRequest request, CancellationToken cancellationToken)
		=> repository
			.SaveTraceAsync(
				gameProviderId: request.GameProviderId,
				walletId: request.WalletId,
				playerCode: request.PlayerCode,
				cancellationToken: cancellationToken)
			.AsTask();
}
