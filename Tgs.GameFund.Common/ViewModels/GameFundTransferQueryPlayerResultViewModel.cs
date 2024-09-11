namespace Tgs.GameFund.Common.ViewModels;
public record GameFundTransferQueryPlayerResultViewModel(
	IEnumerable<GameFundTransferInfoViewModel> Items,
	int TotalCount);
