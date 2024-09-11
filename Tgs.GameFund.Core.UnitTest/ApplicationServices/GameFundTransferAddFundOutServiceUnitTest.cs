using NSubstitute;
using Tgs.GameFund.Core.ApplicationServices;
using Tgs.GameFund.Core.Models;

namespace Tgs.GameFund.Core.UnitTest.ApplicationServices;

public class GameFundTransferAddFundOutServiceUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferRepository>();

		var id = Guid.NewGuid();
		var request = new GameFundTransferAddFundOutRequest(
			Id: id,
			GameProviderId: Guid.NewGuid(),
			WalletId: Guid.NewGuid(),
			PlayerCode: "4ucmqplxb029",
			WalletFrom: "CQ9",
			WalletTo: "Default",
			CashTransactionId: id.ToString(),
			GameTransactionId: Guid.NewGuid().ToString(),
			Amount: 1);

		var info = new GameFundTransferInfo(
			Id: id,
			GameProviderId: request.GameProviderId,
			WalletId: request.WalletId,
			PlayerCode: request.PlayerCode,
			WalletFrom: request.WalletFrom,
			WalletTo: request.WalletTo,
			Common.Enums.GameFundTransferSource.FundOut,
			Common.Enums.GameFundTransferStatus.Processing,
			CashTransactionId: id.ToString(),
			CashRespTransactionId: null,
			GameTransactionId: request.GameTransactionId,
			Amount: 1,
			CreatedOn: DateTimeOffset.UtcNow,
			UpdatedOn: DateTimeOffset.UtcNow);
		_ = fakeGameFundTransferRepository.GetAsync(
			id: Arg.Is(request.Id),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(info);

		var sut = new GameFundTransferAddFundOutService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		var actual = await sut.Handle(request, token);

		Assert.Equal(id, actual.Id);
		Assert.Equal(request.GameProviderId, actual.GameProviderId);
		Assert.Equal(request.WalletId, actual.WalletId);
		Assert.Equal(request.PlayerCode, actual.PlayerCode);
		Assert.Equal(request.WalletFrom, actual.WalletFrom);
		Assert.Equal(request.WalletTo, actual.WalletTo);
		Assert.Equal(Common.Enums.GameFundTransferSource.FundOut, actual.Source);
		Assert.Equal(Common.Enums.GameFundTransferStatus.Processing, actual.Status);
		Assert.Equal(request.CashTransactionId, actual.CashTransactionId);
		Assert.Equal(request.GameTransactionId, actual.GameTransactionId);
		Assert.Equal(1.0m, actual.Amount);

		_ = fakeGameFundTransferRepository
			.Received(1)
			.AddAsync(
			source: Arg.Is(new GameFundTransferAdd(
				Id: request.Id,
				GameProviderId: request.GameProviderId,
				WalletId: request.WalletId,
				PlayerCode: request.PlayerCode,
				WalletFrom: request.WalletFrom,
				WalletTo: request.WalletTo,
				Source: Common.Enums.GameFundTransferSource.FundOut,
				CashTransactionId: request.CashTransactionId,
				GameTransactionId: request.GameTransactionId,
				Amount: request.Amount)),
			cancellationToken: Arg.Any<CancellationToken>());
	}
}
