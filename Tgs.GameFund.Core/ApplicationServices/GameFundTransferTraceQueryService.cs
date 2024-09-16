using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

internal class GameFundTransferTraceQueryService(
	IGameFundTransferTraceRepository repository)
	: IStreamRequestHandler<GameFundTransferTraceQueryRequest, string>
{
	public IAsyncEnumerable<string> Handle(GameFundTransferTraceQueryRequest request, CancellationToken cancellationToken)
		=> request.BrandCode is { Length: > 0 }

			? repository.QueryPlayerCode(
				gameProviderCode: request.GameProviderCode,
				brandCode: request.BrandCode,
				startTime: request.StartTime,
				endTime: request.EndTime,
				cancellationToken: cancellationToken)

			: repository.QueryPlayerCode(
				gameProviderCode: request.GameProviderCode,
				startTime: request.StartTime,
				endTime: request.EndTime,
				cancellationToken: cancellationToken);
}
