using System.Text.Json.Nodes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameFund.Client;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.IntegrationTest.GameFundTransfer;

public class GameFundTransferUpdateByMainWalletRefundSuccessUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var id = Guid.NewGuid();
		var cashRespTransactionId = Guid.NewGuid();
		var gameRespTransactionId = Guid.NewGuid().ToString();
		var mainWalletBalance = 1.0m;
		var description = "test";
		var additionalData = new JsonObject
		{
			["CashTransactionId"] = id.ToString(),
			["GameTransactionId"] = Guid.NewGuid().ToString()
		};

		var fakeMediator = Substitute.For<IMediator>();

		var web = new GameFundTransferApplication(builder => _ = builder.ConfigureServices(services => _ = services.AddTransient(_ => fakeMediator)));

		var httpClient = web.CreateClient();

		var sut = new GameFundClient(httpClient);

		await sut.GameFundTransferUpdateByMainWalletRefundSuccessAsync(
			id: id,
			cashRespTransactionId: cashRespTransactionId,
			gameRespTransactionId: gameRespTransactionId,
			mainWalletBalance: mainWalletBalance,
			description: description,
			additionalData: additionalData);

		_ = fakeMediator
			.Received(1)
			.Send(
			request: Arg.Is<GameFundTransferUpdateByMainWalletRefundSuccessRequest>(compare =>
			compare.Id == id &&
			compare.CashRespTransactionId == cashRespTransactionId &&
			compare.GameRespTransactionId == gameRespTransactionId &&
			compare.MainWalletBalance == mainWalletBalance &&
			compare.Description == description),
			cancellationToken: Arg.Any<CancellationToken>());
	}
}
