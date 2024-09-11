using NSubstitute;
using Tgs.GameFund.Core.ApplicationServices;
using Tgs.GameFund.Core.Models;

namespace Tgs.GameFund.Core.UnitTest.ApplicationServices;

public class GameFundTransferGetServiceUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferRepository>();

		var id = Guid.NewGuid();
		var request = new GameFundTransferGetRequest(
			Id: id);

		var info = new GameFundTransferInfo(
			Id: id,
			GameProviderId: Guid.NewGuid(),
			WalletId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			WalletFrom: "CQ9",
			WalletTo: "Default",
			Source: Common.Enums.GameFundTransferSource.FundOut,
			Status: Common.Enums.GameFundTransferStatus.Processing,
			CashTransactionId: id.ToString(),
			CashRespTransactionId: null,
			GameTransactionId: Guid.NewGuid().ToString(),
			Amount: 1,
			CreatedOn: DateTimeOffset.UtcNow,
			UpdatedOn: DateTimeOffset.UtcNow);
		_ = fakeGameFundTransferRepository.GetAsync(
			id: Arg.Is(request.Id),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(info);

		var sut = new GameFundTransferGetService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		var actual = await sut.Handle(request, token);

		Assert.NotNull(actual);
		Assert.Equal(id, actual.Id);
		Assert.Equal(info.GameProviderId, actual.GameProviderId);
		Assert.Equal(info.WalletId, actual.WalletId);
		Assert.Equal(info.PlayerCode, actual.PlayerCode);
		Assert.Equal(info.WalletFrom, actual.WalletFrom);
		Assert.Equal(info.WalletTo, actual.WalletTo);
		Assert.Equal(Common.Enums.GameFundTransferSource.FundOut, actual.Source);
		Assert.Equal(Common.Enums.GameFundTransferStatus.Processing, actual.Status);
		Assert.Equal(info.CashTransactionId, actual.CashTransactionId);
		Assert.Equal(info.GameTransactionId, actual.GameTransactionId);
		Assert.Equal(1.0m, actual.Amount);
	}

	[Fact]
	public async Task No_Data()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferRepository>();

		var id = Guid.NewGuid();
		var request = new GameFundTransferGetRequest(
			Id: id);
		_ = fakeGameFundTransferRepository.GetAsync(
			id: Arg.Is(request.Id),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(ValueTask.FromResult<GameFundTransferInfo?>(null));

		var sut = new GameFundTransferGetService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		var actual = await sut.Handle(request, token);

		Assert.Null(actual);
	}
}
