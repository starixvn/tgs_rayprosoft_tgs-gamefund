using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

internal class GameFundTransferQueryPlayerService(
	IGameFundTransferRepository repository) : IRequestHandler<GameFundTransferQueryPlayerRequest, GameFundTransferQueryPlayerResponse>
{
	public async Task<GameFundTransferQueryPlayerResponse> Handle(GameFundTransferQueryPlayerRequest request, CancellationToken cancellationToken)
	{
		var infos = await repository.QueryPlayerAsync(
			playerCode: request.PlayerCode,
			startTime: request.StartTime,
			endTime: request.EndTime,
			sort: request.Sort,
			isAscending: request.IsAscending,
			cancellationToken: cancellationToken)
			.ToArrayAsync(
			cancellationToken: cancellationToken)
			.ConfigureAwait(false)
			?? Enumerable.Empty<Models.GameFundTransferInfo>();

		return new GameFundTransferQueryPlayerResponse(
			Items: infos.Skip((request.Page - 1) * request.Rows).Take(request.Rows).Select(x => new GameFundTransferInfoResponse(
				Id: x.Id,
				GameProviderId: x.GameProviderId,
				WalletId: x.WalletId,
				PlayerCode: x.PlayerCode,
				WalletFrom: x.WalletFrom,
				WalletTo: x.WalletTo,
				Source: x.Source,
				Status: x.Status,
				CashTransactionId: x.CashTransactionId,
				GameTransactionId: x.GameTransactionId,
				Amount: x.Amount,
				CreatedOn: x.CreatedOn,
				UpdatedOn: x.UpdatedOn)),
			TotalCount: infos.Count());
	}
}
