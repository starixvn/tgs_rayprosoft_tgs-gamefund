using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

internal class GameFundTransferGetService(
	IGameFundTransferRepository repository) : IRequestHandler<GameFundTransferGetRequest, GameFundTransferInfoResponse?>
{
	public async Task<GameFundTransferInfoResponse?> Handle(GameFundTransferGetRequest request, CancellationToken cancellationToken)
	{
		var info = await repository.GetAsync(
			id: request.Id,
			cancellationToken)
			.ConfigureAwait(false);

		return info is null
			? null
			: new GameFundTransferInfoResponse(
				Id: info.Id,
				GameProviderId: info.GameProviderId,
				WalletId: info.WalletId,
				PlayerCode: info.PlayerCode,
				WalletFrom: info.WalletFrom,
				WalletTo: info.WalletTo,
				Source: info.Source,
				Status: info.Status,
				CashTransactionId: info.CashTransactionId,
				GameTransactionId: info.GameTransactionId,
				Amount: info.Amount,
				CreatedOn: info.CreatedOn,
				UpdatedOn: info.UpdatedOn);
	}
}
