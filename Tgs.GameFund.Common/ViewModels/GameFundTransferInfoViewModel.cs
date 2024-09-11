namespace Tgs.GameFund.Common.ViewModels;
public record GameFundTransferInfoViewModel(
	Guid Id,
	Guid GameProviderId,
	Guid WalletId,
	string PlayerCode,
	string WalletFrom,
	string WalletTo,
	Enums.GameFundTransferSource Source,
	Enums.GameFundTransferStatus Status,
	string CashTransactionId,
	Guid? CashRespTransactionId,
	string GameTransactionId,
	decimal Amount,
	DateTimeOffset CreatedOn,
	DateTimeOffset UpdatedOn);
