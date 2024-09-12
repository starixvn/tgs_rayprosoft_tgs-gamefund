using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tgs.GameFund.Common.ViewModels;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.Controllers;

[Route("[controller]")]
[ApiController]
public class TransferController : ControllerBase
{
	/// <summary>
	/// Games the fund transfer query player asynchronous.
	/// </summary>
	/// <param name="mediator">The mediator.</param>
	/// <param name="source">The source.</param>
	/// <param name="playerCode">The player code.</param>
	/// <returns></returns>
	[HttpGet("{playerCode}")]
	public async ValueTask<GameFundTransferQueryPlayerResultViewModel> GameFundTransferQueryPlayerAsync(
		[FromServices] IMediator mediator,
		[FromQuery] GameFundTransferQueryPlayerViewModel source,
		string playerCode = "4unykr72x8d9")
	{
		var response = await mediator.Send(
			request: new GameFundTransferQueryPlayerRequest(
				PlayerCode: playerCode,
				StartTime: source.StartTime,
				EndTime: source.EndTime,
				Sort: source.Sort,
				IsAscending: source.IsAscending,
				Page: source.Page,
				Rows: source.Rows),
			cancellationToken: HttpContext.RequestAborted)
			.ConfigureAwait(false);

		return new GameFundTransferQueryPlayerResultViewModel(
			Items: response.Items.Select(x => new GameFundTransferInfoViewModel(
				Id: x.Id,
				GameProviderId: x.GameProviderId,
				WalletId: x.WalletId,
				PlayerCode: x.PlayerCode,
				WalletFrom: x.WalletFrom,
				WalletTo: x.WalletTo,
				Source: x.Source,
				Status: x.Status,
				CashTransactionId: x.CashTransactionId,
				CashRespTransactionId: x.CashRespTransactionId,
				GameTransactionId: x.GameTransactionId,
				Amount: x.Amount,
				CreatedOn: x.CreatedOn,
				UpdatedOn: x.UpdatedOn)),
			response.TotalCount);
	}

	/// <summary>
	/// Games the fund transfer get asynchronous.
	/// </summary>
	/// <param name="mediator">The mediator.</param>
	/// <param name="id">The identifier.</param>
	/// <returns></returns>
	[HttpGet("{id:guid}")]
	public async ValueTask<GameFundTransferInfoViewModel?> GameFundTransferGetAsync(
		[FromServices] IMediator mediator,
		Guid id)
	{
		var request = new GameFundTransferGetRequest(Id: id);

		var response = await mediator.Send(
			request: request,
			cancellationToken: HttpContext.RequestAborted)
			.ConfigureAwait(false);

		return response is null
			? null
			: new GameFundTransferInfoViewModel(
				Id: response.Id,
				GameProviderId: response.GameProviderId,
				WalletId: response.WalletId,
				PlayerCode: response.PlayerCode,
				WalletFrom: response.WalletFrom,
				WalletTo: response.WalletTo,
				Source: response.Source,
				Status: response.Status,
				CashTransactionId: response.CashTransactionId,
				CashRespTransactionId: response.CashRespTransactionId,
				GameTransactionId: response.GameTransactionId,
				Amount: response.Amount,
				CreatedOn: response.CreatedOn,
				UpdatedOn: response.UpdatedOn);
	}

	/// <summary>
	/// Games the fund transfer add fund in asynchronous.
	/// </summary>
	/// <param name="mediator">The mediator.</param>
	/// <param name="source">The source.</param>
	/// <returns></returns>
	[HttpPost("add/fund-in")]
	public async ValueTask<GameFundTransferInfoViewModel> GameFundTransferAddFundInAsync(
		[FromServices] IMediator mediator,
		[FromBody] GameFundTransferAddViewModel source)
	{
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

		var response = await mediator.Send(
			request: request,
			cancellationToken: HttpContext.RequestAborted)
			.ConfigureAwait(false);

		return new GameFundTransferInfoViewModel(
			Id: response.Id,
			GameProviderId: response.GameProviderId,
			WalletId: response.WalletId,
			PlayerCode: response.PlayerCode,
			WalletFrom: response.WalletFrom,
			WalletTo: response.WalletTo,
			Source: response.Source,
			Status: response.Status,
			CashTransactionId: response.CashTransactionId,
			CashRespTransactionId: response.CashRespTransactionId,
			GameTransactionId: response.GameTransactionId,
			Amount: response.Amount,
			CreatedOn: response.CreatedOn,
			UpdatedOn: response.UpdatedOn);
	}

	/// <summary>
	/// Games the fund transfer add fund out asynchronous.
	/// </summary>
	/// <param name="mediator">The mediator.</param>
	/// <param name="source">The source.</param>
	/// <returns></returns>
	[HttpPost("add/fund-out")]
	public async ValueTask<GameFundTransferInfoViewModel> GameFundTransferAddFundOutAsync(
		[FromServices] IMediator mediator,
		[FromBody] GameFundTransferAddViewModel source)
	{
		var request = new GameFundTransferAddFundOutRequest(
			Id: source.Id,
			GameProviderId: source.GameProviderId,
			WalletId: source.WalletId,
			PlayerCode: source.PlayerCode,
			WalletFrom: source.WalletFrom,
			WalletTo: source.WalletTo,
			CashTransactionId: source.CashTransactionId,
			GameTransactionId: source.GameTransactionId,
			Amount: source.Amount);

		var response = await mediator.Send(
			request: request,
			cancellationToken: HttpContext.RequestAborted)
			.ConfigureAwait(false);

		return new GameFundTransferInfoViewModel(
			Id: response.Id,
			GameProviderId: response.GameProviderId,
			WalletId: response.WalletId,
			PlayerCode: response.PlayerCode,
			WalletFrom: response.WalletFrom,
			WalletTo: response.WalletTo,
			Source: response.Source,
			Status: response.Status,
			CashTransactionId: response.CashTransactionId,
			CashRespTransactionId: response.CashRespTransactionId,
			GameTransactionId: response.GameTransactionId,
			Amount: response.Amount,
			CreatedOn: response.CreatedOn,
			UpdatedOn: response.UpdatedOn);
	}

	/// <summary>
	/// Games the fund transfer update by game wallet failed asynchronous.
	/// </summary>
	/// <param name="mediator">The mediator.</param>
	/// <param name="id">The identifier.</param>
	/// <param name="source">The source.</param>
	/// <returns></returns>
	[HttpPut("{id:guid}/game-wallet/failed")]
	public async ValueTask GameFundTransferUpdateByGameWalletFailedAsync(
		[FromServices] IMediator mediator,
		Guid id,
		[FromBody] GameFundTransferUpdateByGameWalletFailedViewModel source)
	{
		await mediator.Send(
			request: new GameFundTransferUpdateByGameWalletFailedRequest(
				Id: id,
				Description: source.Description,
				AdditionalData: source.AdditionalData),
			cancellationToken: HttpContext.RequestAborted)
			.ConfigureAwait(false);
	}

	/// <summary>
	/// Games the fund transfer update by main wallet failed asynchronous.
	/// </summary>
	/// <param name="mediator">The mediator.</param>
	/// <param name="id">The identifier.</param>
	/// <param name="source">The source.</param>
	/// <returns></returns>
	[HttpPut("{id:guid}/main-wallet/failed")]
	public async ValueTask GameFundTransferUpdateByMainWalletFailedAsync(
		[FromServices] IMediator mediator,
		Guid id,
		[FromBody] GameFundTransferUpdateByMainWalletFailedViewModel source)
	{
		await mediator.Send(
			request: new GameFundTransferUpdateByMainWalletFailedRequest(
				Id: id,
				Description: source.Description,
				AdditionalData: source.AdditionalData),
			cancellationToken: HttpContext.RequestAborted)
			.ConfigureAwait(false);
	}

	/// <summary>
	/// Games the fund transfer update by main wallet refund failed asynchronous.
	/// </summary>
	/// <param name="mediator">The mediator.</param>
	/// <param name="id">The identifier.</param>
	/// <param name="source">The source.</param>
	/// <returns></returns>
	[HttpPut("{id:guid}/main-wallet/refund/failed")]
	public async ValueTask GameFundTransferUpdateByMainWalletRefundFailedAsync(
		[FromServices] IMediator mediator,
		Guid id,
		[FromBody] GameFundTransferUpdateByMainWalletRefundFailedViewModel source)
	{
		await mediator.Send(
			request: new GameFundTransferUpdateByMainWalletRefundFailedRequest(
				Id: id,
				Description: source.Description,
				AdditionalData: source.AdditionalData),
			cancellationToken: HttpContext.RequestAborted)
			.ConfigureAwait(false);
	}

	/// <summary>
	/// Games the fund transfer update by main wallet refund success asynchronous.
	/// </summary>
	/// <param name="mediator">The mediator.</param>
	/// <param name="id">The identifier.</param>
	/// <param name="source">The source.</param>
	/// <returns></returns>
	[HttpPut("{id:guid}/main-wallet/refund/success")]
	public async ValueTask GameFundTransferUpdateByMainWalletRefundSuccessAsync(
		[FromServices] IMediator mediator,
		Guid id,
		[FromBody] GameFundTransferUpdateByMainWalletRefundSuccessViewModel source)
	{
		await mediator.Send(
			request: new GameFundTransferUpdateByMainWalletRefundSuccessRequest(
				Id: id,
				CashRespTransactionId: source.CashRespTransactionId,
				MainWalletBalance: source.MainWalletBalance,
				Description: source.Description,
				AdditionalData: source.AdditionalData),
			cancellationToken: HttpContext.RequestAborted)
			.ConfigureAwait(false);
	}

	/// <summary>
	/// Games the fund transfer update by success asynchronous.
	/// </summary>
	/// <param name="mediator">The mediator.</param>
	/// <param name="id">The identifier.</param>
	/// <param name="source">The source.</param>
	/// <returns></returns>
	[HttpPut("{id:guid}/success")]
	public async ValueTask GameFundTransferUpdateBySuccessAsync(
		[FromServices] IMediator mediator,
		Guid id,
		[FromBody] GameFundTransferUpdateBySuccessViewModel source)
	{
		await mediator.Send(
			request: new GameFundTransferUpdateBySuccessRequest(
				Id: id,
				CashRespTransactionId: source.CashRespTransactionId,
				GameRespTransactionId: source.GameRespTransactionId,
				MainWalletBalance: source.MainWalletBalance,
				Description: source.Description,
				AdditionalData: source.AdditionalData),
			cancellationToken: HttpContext.RequestAborted)
			.ConfigureAwait(false);
	}
}
