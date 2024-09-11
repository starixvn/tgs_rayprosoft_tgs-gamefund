using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameFund.Client;
using Tgs.GameFund.Common.ViewModels;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.IntegrationTest.GameFundTransfer;

public class GameFundTransferAddFundInUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var id = Guid.NewGuid();
		var source = new GameFundTransferAddViewModel
		{
			Id = id,
			GameProviderId = Guid.NewGuid(),
			WalletId = Guid.NewGuid(),
			PlayerCode = "4ucmqplxb029",
			WalletFrom = "CQ9",
			WalletTo = "Default",
			CashTransactionId = id.ToString(),
			GameTransactionId = Guid.NewGuid().ToString(),
			Amount = 1
		};

		var web = new GameFundTransferApplication(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeMediator = Substitute.For<IMediator>();

			var request = new GameFundTransferAddFundInRequest(
				Id: source.Id,
				GameProviderId: source.GameProviderId,
				WalletId: source.WalletId,
				PlayerCode: source.PlayerCode,
				WalletFrom: source.WalletFrom,
				WalletTo: source.WalletTo,
				CashTransactionId: source.CashTransactionId,
				GameTransactionId: source.GameTransactionId,
				Amount: source.Amount);

			_ = fakeMediator.Send(
				request: Arg.Is(request),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(new GameFundTransferInfoResponse(
				Id: id,
				GameProviderId: request.GameProviderId,
				WalletId: request.WalletId,
				PlayerCode: request.PlayerCode,
				WalletFrom: request.WalletFrom,
				WalletTo: request.WalletTo,
				Source: Common.Enums.GameFundTransferSource.FundIn,
				Status: Common.Enums.GameFundTransferStatus.Processing,
				CashTransactionId: id.ToString(),
				GameTransactionId: request.GameTransactionId,
				Amount: 1,
				CreatedOn: DateTimeOffset.UtcNow,
				UpdatedOn: DateTimeOffset.UtcNow));

			_ = services.AddTransient(_ => fakeMediator);
		}));

		var httpClient = web.CreateClient();

		var sut = new GameFundClient(httpClient);

		var actual = await sut.GameFundTransferAddFundInAsync(
			id: source.Id,
			gameProviderId: source.GameProviderId,
			walletId: source.WalletId,
			playerCode: source.PlayerCode,
			walletFrom: source.WalletFrom,
			walletTo: source.WalletTo,
			cashTransactionId: source.CashTransactionId,
			gameTransactionId: source.GameTransactionId,
			amount: source.Amount);

		Assert.NotNull(actual);
		Assert.Equal(id, actual.Id);
		Assert.Equal(source.GameProviderId, actual.GameProviderId);
		Assert.Equal(source.WalletId, actual.WalletId);
		Assert.Equal(source.PlayerCode, actual.PlayerCode);
		Assert.Equal(source.WalletFrom, actual.WalletFrom);
		Assert.Equal(source.WalletTo, actual.WalletTo);
		Assert.Equal(Common.Enums.GameFundTransferSource.FundIn, actual.Source);
		Assert.Equal(Common.Enums.GameFundTransferStatus.Processing, actual.Status);
		Assert.Equal(source.CashTransactionId, actual.CashTransactionId);
		Assert.Equal(source.GameTransactionId, actual.GameTransactionId);
		Assert.Equal(1.0m, actual.Amount);
	}
}
