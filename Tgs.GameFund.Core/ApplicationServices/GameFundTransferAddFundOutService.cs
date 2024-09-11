using MediatR;
using Tgs.GameFund.Core.Models;

namespace Tgs.GameFund.Core.ApplicationServices;

internal class GameFundTransferAddFundOutService(
	IGameFundTransferRepository repository) : IRequestHandler<GameFundTransferAddFundOutRequest, GameFundTransferInfoResponse>
{
	public async Task<GameFundTransferInfoResponse> Handle(GameFundTransferAddFundOutRequest request, CancellationToken cancellationToken)
	{
		var source = new GameFundTransferAdd(
			Id: request.Id,
			GameProviderId: request.GameProviderId,
			WalletId: request.WalletId,
			PlayerCode: request.PlayerCode,
			WalletFrom: request.WalletFrom,
			WalletTo: request.WalletTo,
			Source: Common.Enums.GameFundTransferSource.FundOut,
			CashTransactionId: request.CashTransactionId,
			GameTransactionId: request.GameTransactionId,
			Amount: request.Amount);

		await repository.AddAsync(source, cancellationToken).ConfigureAwait(false);

		var info = await repository.GetAsync(
			request.Id,
			cancellationToken)
			.ConfigureAwait(false)
			?? throw new ArgumentNullException(request.Id.ToString());

		return new GameFundTransferInfoResponse(
			Id: info.Id,
			GameProviderId: info.GameProviderId,
			WalletId: info.WalletId,
			PlayerCode: info.PlayerCode,
			WalletFrom: info.WalletFrom,
			WalletTo: info.WalletTo,
			Source: info.Source,
			Status: info.Status,
			CashTransactionId: info.CashTransactionId,
			CashRespTransactionId: info.CashRespTransactionId,
			GameTransactionId: info.GameTransactionId,
			Amount: info.Amount,
			CreatedOn: info.CreatedOn,
			UpdatedOn: info.UpdatedOn);
	}
}
