using System.Text.Json.Nodes;
using NSubstitute;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.Core.UnitTest.ApplicationServices;

public class GameFundTransferUpdateByMainWalletFailedServiceUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferRepository>();

		var id = Guid.NewGuid();
		var request = new GameFundTransferUpdateByMainWalletFailedRequest(
			Id: id,
			Description: string.Empty,
			AdditionalData: new JsonObject
			{
				["CashTransactionId"] = id.ToString(),
				["GameTransactionId"] = Guid.NewGuid().ToString()
			});

		var sut = new GameFundTransferUpdateByMainWalletFailedService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		await sut.Handle(request, token);

		_ = fakeGameFundTransferRepository
			.Received(1)
			.UpdateByMainWalletFailedAsync(
			id: Arg.Is(request.Id),
			cancellationToken: Arg.Any<CancellationToken>());
	}
}
