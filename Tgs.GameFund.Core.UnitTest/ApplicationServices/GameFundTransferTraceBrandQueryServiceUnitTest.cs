using NSubstitute;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.Core.UnitTest.ApplicationServices;

public class GameFundTransferTraceBrandQueryServiceUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferTraceRepository>();

		var response = "player_code01";
		var request = new GameFundTransferTraceBrandQueryRequest(
			GameProviderCode: "provider01",
			BrandCode: "brand01");

		fakeGameFundTransferRepository
			.QueryPlayerCode(
				gameProviderCode: Arg.Any<string>(),
				brandCode: Arg.Any<string>(),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(new[] { response }.ToAsyncEnumerable());

		var sut = new GameFundTransferTraceBrandQueryService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		var actual = await sut
			.Handle(request, token)
			.ToArrayAsync(cancellationToken: default);

		Assert.Equal("player_code01", actual[0]);
		_ = fakeGameFundTransferRepository
			.Received(1)
			.QueryPlayerCode(
				gameProviderCode: Arg.Is("provider01"),
				brandCode: Arg.Is("brand01"),
				cancellationToken: Arg.Any<CancellationToken>());
	}
}
