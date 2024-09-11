namespace Tgs.GameFund.Common.ViewModels;
public record GameFundTransferQueryOperatorResultViewModel(
	IEnumerable<GameFundTransferInfoViewModel> Items,
	int TotalCount);
