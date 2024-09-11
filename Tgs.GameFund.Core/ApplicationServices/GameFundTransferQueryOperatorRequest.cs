using MediatR;

namespace Tgs.GameFund.Core.ApplicationServices;
public record GameFundTransferQueryOperatorRequest(
	string PlayerCode,
	DateTimeOffset StartTime,
	DateTimeOffset EndTime,
	Common.Enums.GameFundTransferSort Sort,
	bool IsAscending = true,
	int Page = 1,
	int Rows = 20) : IRequest<GameFundTransferQueryOperatorResponse>;
