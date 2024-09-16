using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;

public record class GameFundTransferTraceMonthQueryRequest(
	string GameProviderCode,
	int Year,
	int Month)
	: IStreamRequest<string>;
