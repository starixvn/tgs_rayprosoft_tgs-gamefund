namespace Tgs.GameFund.Core.ApplicationServices;
public record GameFundTransferQueryPlayerResponse(
	IEnumerable<GameFundTransferInfoResponse> Items,
	int TotalCount);
