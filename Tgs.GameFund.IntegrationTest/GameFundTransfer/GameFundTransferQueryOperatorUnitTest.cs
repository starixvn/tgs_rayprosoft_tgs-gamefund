using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameFund.Client;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.IntegrationTest.GameFundTransfer;

public class GameFundTransferQueryOperatorUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var id = Guid.NewGuid();

		var playerCode = "4ucmqplxb029";
		var startTime = DateTimeOffset.UtcNow.AddMonths(-1);
		var endTime = DateTimeOffset.UtcNow;
		var sort = Common.Enums.GameFundTransferSort.CreatedOn;
		var isAscending = true;
		var page = 1;
		var rows = 1;

		var infoResponse = new GameFundTransferInfoResponse(
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

		var response = new GameFundTransferQueryOperatorResponse(
			Items: [infoResponse],
			TotalCount: 1);

		var web = new GameFundTransferApplication(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeMediator = Substitute.For<IMediator>();

			var request = new GameFundTransferQueryOperatorRequest(
				PlayerCode: playerCode,
				StartTime: DateTimeOffset.Parse(startTime.ToString("u")),
				EndTime: DateTimeOffset.Parse(endTime.ToString("u")),
				Sort: sort,
				IsAscending: isAscending,
				Page: page,
				Rows: rows);

			_ = fakeMediator.Send(
				request: Arg.Is(request),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(response);

			_ = services.AddTransient(_ => fakeMediator);
		}));

		var httpClient = web.CreateClient();

		var sut = new GameFundClient(httpClient);

		var actual = await sut.GameFundTransferQueryOperatorAsync(
			playerCode: playerCode,
			startTime: startTime,
			endTime: endTime,
			sort: sort,
			isAscending: isAscending,
			page: page,
			rows: rows);

		Assert.NotNull(actual);
		Assert.Equal(1, actual.TotalCount);
		Assert.NotEmpty(actual.Items.ToArray());
		Assert.Equal(id, actual.Items.ToArray()[0].Id);
		Assert.Equal(infoResponse.GameProviderId, actual.Items.ToArray()[0].GameProviderId);
		Assert.Equal(infoResponse.WalletId, actual.Items.ToArray()[0].WalletId);
		Assert.Equal(infoResponse.PlayerCode, actual.Items.ToArray()[0].PlayerCode);
		Assert.Equal(infoResponse.WalletFrom, actual.Items.ToArray()[0].WalletFrom);
		Assert.Equal(infoResponse.WalletTo, actual.Items.ToArray()[0].WalletTo);
		Assert.Equal(Common.Enums.GameFundTransferSource.FundIn, actual.Items.ToArray()[0].Source);
		Assert.Equal(Common.Enums.GameFundTransferStatus.Processing, actual.Items.ToArray()[0].Status);
		Assert.Equal(infoResponse.CashTransactionId, actual.Items.ToArray()[0].CashTransactionId);
		Assert.Equal(infoResponse.GameTransactionId, actual.Items.ToArray()[0].GameTransactionId);
		Assert.Equal(1.0m, actual.Items.ToArray()[0].Amount);
	}

	[Fact]
	public async Task No_Data()
	{
		var id = Guid.NewGuid();

		var playerCode = "4ucmqplxb029";
		var startTime = DateTimeOffset.UtcNow.AddMonths(-1);
		var endTime = DateTimeOffset.UtcNow;
		var sort = Common.Enums.GameFundTransferSort.CreatedOn;
		var isAscending = true;
		var page = 1;
		var rows = 1;

		var response = new GameFundTransferQueryOperatorResponse(
			Items: [],
			TotalCount: 0);

		var web = new GameFundTransferApplication(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeMediator = Substitute.For<IMediator>();

			var request = new GameFundTransferQueryOperatorRequest(
				PlayerCode: playerCode,
				StartTime: DateTimeOffset.Parse(startTime.ToString("u")),
				EndTime: DateTimeOffset.Parse(endTime.ToString("u")),
				Sort: sort,
				IsAscending: isAscending,
				Page: page,
				Rows: rows);

			_ = fakeMediator.Send(
				request: Arg.Is(request),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(response);

			_ = services.AddTransient(_ => fakeMediator);
		}));

		var httpClient = web.CreateClient();

		var sut = new GameFundClient(httpClient);

		var actual = await sut.GameFundTransferQueryOperatorAsync(
			playerCode: playerCode,
			startTime: startTime,
			endTime: endTime,
			sort: sort,
			isAscending: isAscending,
			page: page,
			rows: rows);

		Assert.NotNull(actual);
		Assert.Equal(0, actual.TotalCount);
		Assert.Empty(actual.Items.ToArray());
	}
}
