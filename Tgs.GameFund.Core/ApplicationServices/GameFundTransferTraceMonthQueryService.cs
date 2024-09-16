using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

internal class GameFundTransferTraceMonthQueryService(
	IGameFundTransferTraceRepository repository)
	: IStreamRequestHandler<GameFundTransferTraceMonthQueryRequest, string>
{
	public IAsyncEnumerable<string> Handle(GameFundTransferTraceMonthQueryRequest request, CancellationToken cancellationToken)
		=> repository
			.QueryPlayerCode(
				gameProviderCode: request.GameProviderCode,
				year: request.Year,
				month: request.Month,
				cancellationToken: cancellationToken);
}
