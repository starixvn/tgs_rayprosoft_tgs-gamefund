namespace Tgs.GameFund.Core.ApplicationServices;
public record GameFundTransferQueryOperatorResponse(
	IEnumerable<GameFundTransferInfoResponse> Items,
	int TotalCount);
