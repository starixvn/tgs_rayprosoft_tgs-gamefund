using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameFund.Client;
using Tgs.GameFund.Common.ViewModels;
using Tgs.GameFund.Core.ApplicationServices;
using Tgs.GameFund.Entities;

namespace Tgs.GameFund.IntegrationTest.GameFundTransfer;

public class GameFundTransferGetUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var id = Guid.NewGuid();

		var response = new GameFundTransferInfoResponse(
			Id: id,
			GameProviderId: Guid.NewGuid(),
			WalletId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			WalletFrom: "CQ9",
			WalletTo: "Default",
			Source: Common.Enums.GameFundTransferSource.FundIn,
			Status: Common.Enums.GameFundTransferStatus.Processing,
			CashTransactionId: id.ToString(),
			GameTransactionId: Guid.NewGuid().ToString(),
			Amount: 1,
			CreatedOn: DateTimeOffset.UtcNow,
			UpdatedOn: DateTimeOffset.UtcNow);

		var web = new GameFundTransferApplication(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeMediator = Substitute.For<IMediator>();

			var request = new GameFundTransferGetRequest(
				Id: id);

			_ = fakeMediator.Send(
				request: Arg.Is(request),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(response);

			_ = services.AddTransient(_ => fakeMediator);
		}));

		var httpClient = web.CreateClient();

		var sut = new GameFundClient(httpClient);

		var actual = await sut.GameFundTransferGetAsync(
			id: id);

		Assert.NotNull(actual);
		Assert.Equal(id, actual.Id);
		Assert.Equal(response.GameProviderId, actual.GameProviderId);
		Assert.Equal(response.WalletId, actual.WalletId);
		Assert.Equal(response.PlayerCode, actual.PlayerCode);
		Assert.Equal(response.WalletFrom, actual.WalletFrom);
		Assert.Equal(response.WalletTo, actual.WalletTo);
		Assert.Equal(Common.Enums.GameFundTransferSource.FundIn, actual.Source);
		Assert.Equal(Common.Enums.GameFundTransferStatus.Processing, actual.Status);
		Assert.Equal(response.CashTransactionId, actual.CashTransactionId);
		Assert.Equal(response.GameTransactionId, actual.GameTransactionId);
		Assert.Equal(1.0m, actual.Amount);
	}
}
