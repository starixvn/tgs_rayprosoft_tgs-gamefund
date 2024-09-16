using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameFund.Client;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.IntegrationTest.GameFundTransferTrace;

public class GameFundTransferTraceQueryUnitTest
{
	[Fact]
	public async Task ProviderOnly()
	{
		var gameProviderCode = "provider01";
		var response = "player_code01";

		var web = new GameFundTransferApplication(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeMediator = Substitute.For<IMediator>();

			var request = new GameFundTransferTraceQueryRequest(
				GameProviderCode: gameProviderCode,
				BrandCode: null,
				StartTime: null,
				EndTime: null);

			_ = fakeMediator
				.CreateStream(
					request: Arg.Is(request),
					cancellationToken: Arg.Any<CancellationToken>())
				.Returns(new[] { response }.ToAsyncEnumerable());

			_ = services.AddTransient(_ => fakeMediator);
		}));

		var httpClient = web.CreateClient();

		var sut = new GameFundClient(httpClient);

		var actual = await sut.GameFundTransferTraceQueryAsync(
			gameProviderCode: gameProviderCode,
			brandCode: null,
			startTime: null,
			endTime: null);

		Assert.NotNull(actual);
		Assert.Single(actual);
		Assert.Equal("player_code01", actual[0]);
	}

	[Fact]
	public async Task ProviderAndStartEnd()
	{
		var gameProviderCode = "provider01";
		var startTime = new DateTimeOffset(year: 2024, month: 09, day: 16, hour: 0, minute: 0, second: 0, TimeSpan.FromHours(8));
		var endTime = new DateTimeOffset(year: 2024, month: 09, day: 16, hour: 12, minute: 0, second: 0, TimeSpan.FromHours(8));
		var response = "player_code01";

		var web = new GameFundTransferApplication(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeMediator = Substitute.For<IMediator>();

			var request = new GameFundTransferTraceQueryRequest(
				GameProviderCode: gameProviderCode,
				BrandCode: null,
				StartTime: startTime,
				EndTime: endTime);

			_ = fakeMediator
				.CreateStream(
					request: Arg.Is(request),
					cancellationToken: Arg.Any<CancellationToken>())
				.Returns(new[] { response }.ToAsyncEnumerable());

			_ = services.AddTransient(_ => fakeMediator);
		}));

		var httpClient = web.CreateClient();

		var sut = new GameFundClient(httpClient);

		var actual = await sut.GameFundTransferTraceQueryAsync(
			gameProviderCode: gameProviderCode,
			brandCode: null,
			startTime: startTime,
			endTime: endTime);

		Assert.NotNull(actual);
		Assert.Single(actual);
		Assert.Equal("player_code01", actual[0]);
	}

	[Fact]
	public async Task ProviderAndBrand()
	{
		var gameProviderCode = "provider01";
		var brandCode = "brand01";
		var response = "player_code01";

		var web = new GameFundTransferApplication(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeMediator = Substitute.For<IMediator>();

			var request = new GameFundTransferTraceQueryRequest(
				GameProviderCode: gameProviderCode,
				BrandCode: brandCode,
				StartTime: null,
				EndTime: null);

			_ = fakeMediator
				.CreateStream(
					request: Arg.Is(request),
					cancellationToken: Arg.Any<CancellationToken>())
				.Returns(new[] { response }.ToAsyncEnumerable());

			_ = services.AddTransient(_ => fakeMediator);
		}));

		var httpClient = web.CreateClient();

		var sut = new GameFundClient(httpClient);

		var actual = await sut.GameFundTransferTraceQueryAsync(
			gameProviderCode: gameProviderCode,
			brandCode: brandCode,
			startTime: null,
			endTime: null);

		Assert.NotNull(actual);
		Assert.Single(actual);
		Assert.Equal("player_code01", actual[0]);
	}

	[Fact]
	public async Task ProviderAndBrandAndStartEnd()
	{
		var gameProviderCode = "provider01";
		var brandCode = "brand01";
		var startTime = new DateTimeOffset(year: 2024, month: 09, day: 16, hour: 0, minute: 0, second: 0, TimeSpan.FromHours(8));
		var endTime = new DateTimeOffset(year: 2024, month: 09, day: 16, hour: 12, minute: 0, second: 0, TimeSpan.FromHours(8));
		var response = "player_code01";

		var web = new GameFundTransferApplication(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeMediator = Substitute.For<IMediator>();

			var request = new GameFundTransferTraceQueryRequest(
				GameProviderCode: gameProviderCode,
				BrandCode: brandCode,
				StartTime: startTime,
				EndTime: endTime);

			_ = fakeMediator
				.CreateStream(
					request: Arg.Is(request),
					cancellationToken: Arg.Any<CancellationToken>())
				.Returns(new[] { response }.ToAsyncEnumerable());

			_ = services.AddTransient(_ => fakeMediator);
		}));

		var httpClient = web.CreateClient();

		var sut = new GameFundClient(httpClient);

		var actual = await sut.GameFundTransferTraceQueryAsync(
			gameProviderCode: gameProviderCode,
			brandCode: brandCode,
			startTime: startTime,
			endTime: endTime);

		Assert.NotNull(actual);
		Assert.Single(actual);
		Assert.Equal("player_code01", actual[0]);
	}
}
