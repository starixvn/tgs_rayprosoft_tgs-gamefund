using NSubstitute;
using Tgs.GameFund.Core.ApplicationServices;
using Tgs.GameFund.Core.Models;

namespace Tgs.GameFund.Core.UnitTest.ApplicationServices;

public class GameFundTransferQueryPlayerServiceUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferRepository>();

		var id = Guid.NewGuid();
		var request = new GameFundTransferQueryPlayerRequest(
			PlayerCode: "4ucmqplxb029",
			StartTime: DateTimeOffset.UtcNow.AddMonths(-1),
			EndTime: DateTimeOffset.UtcNow,
			Sort: Common.Enums.GameFundTransferSort.CreatedOn,
			IsAscending: true,
			Page: 1,
			Rows: 1);

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
			GameTransactionId: Guid.NewGuid().ToString(),
			Amount: 1,
			CreatedOn: DateTimeOffset.UtcNow,
			UpdatedOn: DateTimeOffset.UtcNow);
		_ = fakeGameFundTransferRepository.QueryPlayerAsync(
			playerCode: Arg.Is(request.PlayerCode),
			startTime: Arg.Is(request.StartTime),
			endTime: Arg.Is(request.EndTime),
			sort: Arg.Is(request.Sort),
			isAscending: Arg.Is(request.IsAscending),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(new[] { info }.ToAsyncEnumerable());

		var sut = new GameFundTransferQueryPlayerService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		var actual = await sut.Handle(request, token);

		Assert.NotNull(actual);
		Assert.Equal(1, actual.TotalCount);
		Assert.NotEmpty(actual.Items.ToArray());
		Assert.Equal(id, actual.Items.ToArray()[0].Id);
		Assert.Equal(info.GameProviderId, actual.Items.ToArray()[0].GameProviderId);
		Assert.Equal(info.WalletId, actual.Items.ToArray()[0].WalletId);
		Assert.Equal(info.PlayerCode, actual.Items.ToArray()[0].PlayerCode);
		Assert.Equal(info.WalletFrom, actual.Items.ToArray()[0].WalletFrom);
		Assert.Equal(info.WalletTo, actual.Items.ToArray()[0].WalletTo);
		Assert.Equal(Common.Enums.GameFundTransferSource.FundOut, actual.Items.ToArray()[0].Source);
		Assert.Equal(Common.Enums.GameFundTransferStatus.Processing, actual.Items.ToArray()[0].Status);
		Assert.Equal(info.CashTransactionId, actual.Items.ToArray()[0].CashTransactionId);
		Assert.Equal(info.GameTransactionId, actual.Items.ToArray()[0].GameTransactionId);
		Assert.Equal(1.0m, actual.Items.ToArray()[0].Amount);
	}

	[Fact]
	public async Task Page_2_Rows_1_No_Data()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferRepository>();

		var id = Guid.NewGuid();
		var request = new GameFundTransferQueryPlayerRequest(
			PlayerCode: "4ucmqplxb029",
			StartTime: DateTimeOffset.UtcNow.AddMonths(-1),
			EndTime: DateTimeOffset.UtcNow,
			Sort: Common.Enums.GameFundTransferSort.CreatedOn,
			IsAscending: true,
			Page: 2,
			Rows: 1);

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
			GameTransactionId: Guid.NewGuid().ToString(),
			Amount: 1,
			CreatedOn: DateTimeOffset.UtcNow,
			UpdatedOn: DateTimeOffset.UtcNow);
		_ = fakeGameFundTransferRepository.QueryPlayerAsync(
			playerCode: Arg.Is(request.PlayerCode),
			startTime: Arg.Is(request.StartTime),
			endTime: Arg.Is(request.EndTime),
			sort: Arg.Is(request.Sort),
			isAscending: Arg.Is(request.IsAscending),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(new[] { info }.ToAsyncEnumerable());

		var sut = new GameFundTransferQueryPlayerService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		var actual = await sut.Handle(request, token);

		Assert.NotNull(actual);
		Assert.Equal(1, actual.TotalCount);
		Assert.Empty(actual.Items.ToArray());
	}

	[Fact]
	public async Task No_Data()
	{
		var fakeGameFundTransferRepository = Substitute.For<IGameFundTransferRepository>();

		var request = new GameFundTransferQueryPlayerRequest(
			PlayerCode: "4ucmqplxb029",
			StartTime: DateTimeOffset.UtcNow.AddMonths(-1),
			EndTime: DateTimeOffset.UtcNow,
			Sort: Common.Enums.GameFundTransferSort.CreatedOn,
			IsAscending: true,
			Page: 1,
			Rows: 1);

		_ = fakeGameFundTransferRepository.QueryPlayerAsync(
			playerCode: Arg.Is(request.PlayerCode),
			startTime: Arg.Is(request.StartTime),
			endTime: Arg.Is(request.EndTime),
			sort: Arg.Is(request.Sort),
			isAscending: Arg.Is(request.IsAscending),
			cancellationToken: Arg.Any<CancellationToken>())
			.Returns(Enumerable.Empty<GameFundTransferInfo>().ToAsyncEnumerable());

		var sut = new GameFundTransferQueryPlayerService(
			fakeGameFundTransferRepository);

		var cancellationTokenSource = new CancellationTokenSource();
		var token = cancellationTokenSource.Token;

		var actual = await sut.Handle(request, token);

		Assert.NotNull(actual);
		Assert.Equal(0, actual.TotalCount);
		Assert.Empty(actual.Items.ToArray());
	}
}
