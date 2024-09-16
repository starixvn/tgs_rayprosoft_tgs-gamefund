using NSubstitute;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.Core.UnitTest.ApplicationServices;

public class GameFundTransferTraceMonthQueryServiceUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferTraceRepository>();

		var response = "player_code01";
		var request = new GameFundTransferTraceMonthQueryRequest(
			GameProviderCode: "provider01",
			Year: 2024,
			Month: 9);

		fakeGameFundTransferRepository
			.QueryPlayerCode(
				gameProviderCode: Arg.Any<string>(),
				year: Arg.Any<int>(),
				month: Arg.Any<int>(),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(new[] { response }.ToAsyncEnumerable());

		var sut = new GameFundTransferTraceMonthQueryService(
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
				year: Arg.Is(2024),
				month: Arg.Is(9),
				cancellationToken: Arg.Any<CancellationToken>());
	}
}
