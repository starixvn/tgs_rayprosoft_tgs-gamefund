using NSubstitute;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.Core.UnitTest.ApplicationServices;

public class GameFundTransferSaveTraceServiceUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferRepository>();

		var request = new GameFundTransferSaveTraceRequest(
			GameProviderId: Guid.NewGuid(),
			WalletId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029");

		var sut = new GameFundTransferSaveTraceService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		await sut.Handle(request, token);

		_ = fakeGameFundTransferRepository
			.Received(1)
			.SaveTraceAsync(
				gameProviderId: Arg.Is(request.GameProviderId),
				walletId: Arg.Is(request.WalletId),
				playerCode: Arg.Is(request.PlayerCode),
				cancellationToken: Arg.Any<CancellationToken>());
	}
}
