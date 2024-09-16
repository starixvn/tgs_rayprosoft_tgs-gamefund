using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tgs.GameFund.Common.ViewModels;
using Tgs.GameFund.Core.ApplicationServices;

namespace Tgs.GameFund.Controllers;

[Route("[controller]")]
[ApiController]
public class TransferTraceController : ControllerBase
{
	[HttpPut("trace/save")]
	public async ValueTask GameFundTransferTraceSaveAsync(
		[FromServices] IMediator mediator,
		[FromBody] GameFundTransferTraceSaveViewModel source)
	{
		await mediator.Send(
			request: new GameFundTransferTraceSaveRequest(
				GameProviderId: source.GameProviderId,
				WalletId: source.WalletId,
				PlayerCode: source.PlayerCode),
			cancellationToken: HttpContext.RequestAborted)
			.ConfigureAwait(false);
	}

	[HttpGet("trace/queryWithBrand")]
	public IAsyncEnumerable<string> GameFundTransferTraceQueryBrandAsync(
		[FromServices] IMediator mediator,
		[FromQuery] string gameProviderCode,
		[FromQuery] string brandCode)
		=> mediator.CreateStream(
			request: new GameFundTransferTraceBrandQueryRequest(
				GameProviderCode: gameProviderCode,
				BrandCode: brandCode),
			cancellationToken: HttpContext.RequestAborted);

	[HttpGet("trace/queryWithMonth")]
	public IAsyncEnumerable<string> GameFundTransferTraceQueryMonthAsync(
		[FromServices] IMediator mediator,
		[FromQuery] string gameProviderCode,
		[FromQuery] int year,
		[FromQuery] int month)
		=> mediator.CreateStream(
			request: new GameFundTransferTraceMonthQueryRequest(
				GameProviderCode: gameProviderCode,
				Year: year,
				Month: month),
			cancellationToken: HttpContext.RequestAborted);
}
