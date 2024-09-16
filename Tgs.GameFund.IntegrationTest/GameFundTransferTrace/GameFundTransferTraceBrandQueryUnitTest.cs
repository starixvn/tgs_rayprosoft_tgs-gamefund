using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameFund.Client;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.IntegrationTest.GameFundTransferTrace;

public class GameFundTransferTraceBrandQueryUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var gameProviderCode = "provider01";
		var brandCode = "brand01";
		var response = new[]
		{
			"player_code01",
		};

		var web = new GameFundTransferApplication(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeMediator = Substitute.For<IMediator>();

			var request = new GameFundTransferTraceBrandQueryRequest(
				GameProviderCode: gameProviderCode,
				BrandCode: brandCode);

			_ = fakeMediator.CreateStream(
				request: Arg.Is(request),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(new[] { "player_code01" }.ToAsyncEnumerable());

			_ = services.AddTransient(_ => fakeMediator);
		}));

		var httpClient = web.CreateClient();

		var sut = new GameFundClient(httpClient);

		var actual = await sut.GameFundTransferTraceBrandQueryAsync(
			gameProviderCode: gameProviderCode,
			brandCode: brandCode);

		Assert.NotNull(actual);
		Assert.Single(actual);
		Assert.Equal("player_code01", actual[0]);
	}
}
