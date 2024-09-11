using System.Text.Json.Nodes;
using NSubstitute;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.Core.UnitTest.ApplicationServices;

public class GameFundTransferUpdateByMainWalletRefundSuccessServiceUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferRepository>();

		var id = Guid.NewGuid();
		var request = new GameFundTransferUpdateByMainWalletRefundSuccessRequest(
			Id: id,
			CashRespTransactionId: Guid.NewGuid(),
			GameRespTransactionId: Guid.NewGuid().ToString(),
			MainWalletBalance: 1.0m,
			Description: string.Empty,
			AdditionalData: new JsonObject
			{
				["CashTransactionId"] = id.ToString(),
				["GameTransactionId"] = Guid.NewGuid().ToString()
			});

		var sut = new GameFundTransferUpdateByMainWalletRefundSuccessService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		await sut.Handle(request, token);

		_ = fakeGameFundTransferRepository
			.Received(1)
			.UpdateByMainWalletRefundSuccessAsync(
			id: Arg.Is(request.Id),
			cashRespTransactionId: Arg.Is(request.CashRespTransactionId),
			balance: Arg.Is(request.MainWalletBalance),
			description: Arg.Is(request.Description),
			cancellationToken: Arg.Any<CancellationToken>());
	}
}
