using NSubstitute;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.Core.UnitTest.ApplicationServices;

public class GameFundTransferTraceQueryServiceUnitTest
{
	[Fact]
	public async Task ProviderOnly()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferTraceRepository>();

		var request = new GameFundTransferTraceQueryRequest(
			GameProviderCode: "provider01");
		var response = "player_code01";

		fakeGameFundTransferRepository
			.QueryPlayerCode(
				gameProviderCode: Arg.Any<string>(),
				startTime: null,
				endTime: null,
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(new[] { response }.ToAsyncEnumerable());

		var sut = new GameFundTransferTraceQueryService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		var actual = await sut
			.Handle(request, token)
			.ToArrayAsync(cancellationToken: default);

		Assert.NotNull(actual);
		Assert.Single(actual);
		Assert.Equal("player_code01", actual[0]);
		_ = fakeGameFundTransferRepository
			.Received(1)
			.QueryPlayerCode(
				gameProviderCode: Arg.Is("provider01"),
				startTime: null,
				endTime: null,
				cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task ProviderAndStartEnd()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferTraceRepository>();

		var request = new GameFundTransferTraceQueryRequest(
			GameProviderCode: "provider01",
			StartTime: new DateTimeOffset(year: 2024, month: 09, day: 16, hour: 0, minute: 0, second: 0, TimeSpan.FromHours(8)),
			EndTime: new DateTimeOffset(year: 2024, month: 09, day: 16, hour: 12, minute: 0, second: 0, TimeSpan.FromHours(8)));
		var response = "player_code01";

		fakeGameFundTransferRepository
			.QueryPlayerCode(
				gameProviderCode: Arg.Any<string>(),
				startTime: Arg.Any<DateTimeOffset>(),
				endTime: Arg.Any<DateTimeOffset>(),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(new[] { response }.ToAsyncEnumerable());

		var sut = new GameFundTransferTraceQueryService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		var actual = await sut
			.Handle(request, token)
			.ToArrayAsync(cancellationToken: default);

		Assert.NotNull(actual);
		Assert.Single(actual);
		Assert.Equal("player_code01", actual[0]);
		_ = fakeGameFundTransferRepository
			.Received(1)
			.QueryPlayerCode(
				gameProviderCode: Arg.Is("provider01"),
				startTime: Arg.Is(new DateTimeOffset(year: 2024, month: 09, day: 16, hour: 0, minute: 0, second: 0, TimeSpan.FromHours(8))),
				endTime: Arg.Is(new DateTimeOffset(year: 2024, month: 09, day: 16, hour: 12, minute: 0, second: 0, TimeSpan.FromHours(8))),
				cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task ProviderAndBran()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferTraceRepository>();

		var request = new GameFundTransferTraceQueryRequest(
			GameProviderCode: "provider01",
			BrandCode: "brand01");
		var response = "player_code01";

		fakeGameFundTransferRepository
			.QueryPlayerCode(
				gameProviderCode: Arg.Any<string>(),
				brandCode: Arg.Any<string>(),
				startTime: null,
				endTime: null,
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(new[] { response }.ToAsyncEnumerable());

		var sut = new GameFundTransferTraceQueryService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		var actual = await sut
			.Handle(request, token)
			.ToArrayAsync(cancellationToken: default);

		Assert.NotNull(actual);
		Assert.Single(actual);
		Assert.Equal("player_code01", actual[0]);
		_ = fakeGameFundTransferRepository
			.Received(1)
			.QueryPlayerCode(
				gameProviderCode: Arg.Is("provider01"),
				brandCode: Arg.Is("brand01"),
				startTime: null,
				endTime: null,
				cancellationToken: Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task ProviderAndBrandAndStartEnd()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferTraceRepository>();

		var request = new GameFundTransferTraceQueryRequest(
			GameProviderCode: "provider01",
			BrandCode: "brand01",
			StartTime: new DateTimeOffset(year: 2024, month: 09, day: 16, hour: 0, minute: 0, second: 0, TimeSpan.FromHours(8)),
			EndTime: new DateTimeOffset(year: 2024, month: 09, day: 16, hour: 12, minute: 0, second: 0, TimeSpan.FromHours(8)));
		var response = "player_code01";

		fakeGameFundTransferRepository
			.QueryPlayerCode(
				gameProviderCode: Arg.Any<string>(),
				brandCode: Arg.Any<string>(),
				startTime: Arg.Any<DateTimeOffset>(),
				endTime: Arg.Any<DateTimeOffset>(),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(new[] { response }.ToAsyncEnumerable());

		var sut = new GameFundTransferTraceQueryService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		var actual = await sut
			.Handle(request, token)
			.ToArrayAsync(cancellationToken: default);

		Assert.NotNull(actual);
		Assert.Single(actual);
		Assert.Equal("player_code01", actual[0]);
		_ = fakeGameFundTransferRepository
			.Received(1)
			.QueryPlayerCode(
				gameProviderCode: Arg.Is("provider01"),
				brandCode: Arg.Is("brand01"),
				startTime: Arg.Is(new DateTimeOffset(year: 2024, month: 09, day: 16, hour: 0, minute: 0, second: 0, TimeSpan.FromHours(8))),
				endTime: Arg.Is(new DateTimeOffset(year: 2024, month: 09, day: 16, hour: 12, minute: 0, second: 0, TimeSpan.FromHours(8))),
				cancellationToken: Arg.Any<CancellationToken>());
	}
}
