using System.Text.Json.Nodes;
using NSubstitute;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.Core.UnitTest.ApplicationServices;

public class GameFundTransferUpdateByGameWalletFailedServiceUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferRepository>();

		var id = Guid.NewGuid();
		var request = new GameFundTransferUpdateByGameWalletFailedRequest(
			Id: id,
			Description: string.Empty,
			AdditionalData: new JsonObject
			{
				["CashTransactionId"] = id.ToString(),
				["GameTransactionId"] = Guid.NewGuid().ToString()
			});

		var sut = new GameFundTransferUpdateByGameWalletFailedService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		await sut.Handle(request, token);

		_ = fakeGameFundTransferRepository
			.Received(1)
			.UpdateByGameWalletFailedAsync(
			id: Arg.Is(request.Id),
			cancellationToken: Arg.Any<CancellationToken>());
	}
}
