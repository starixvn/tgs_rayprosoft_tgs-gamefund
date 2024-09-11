using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameFund.Client;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.IntegrationTest.GameFundTransfer;

public class GameFundTransferSaveTraceUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var gameProviderId = Guid.NewGuid();
		var walletId = Guid.NewGuid();
		var playerCode = "4ucmqplxb029";

		var fakeMediator = Substitute.For<IMediator>();

		var web = new GameFundTransferApplication(builder => _ = builder.ConfigureServices(services => _ = services.AddTransient(_ => fakeMediator)));

		var httpClient = web.CreateClient();

		var sut = new GameFundClient(httpClient);

		await sut.GameFundTransferSaveTraceAsync(
			gameProviderId: gameProviderId,
			walletId: walletId,
			playerCode: playerCode);

		_ = fakeMediator
			.Received(1)
			.Send(
				request: Arg.Is<GameFundTransferSaveTraceRequest>(compare =>
					compare.GameProviderId == gameProviderId &&
					compare.WalletId == walletId &&
					compare.PlayerCode == playerCode),
					cancellationToken: Arg.Any<CancellationToken>());
	}
}
