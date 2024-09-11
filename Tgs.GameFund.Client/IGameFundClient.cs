using System.Text.Json.Nodes;

namespace Tgs.GameFund.Client;

public interface IGameFundClient
{
	public ValueTask<Common.ViewModels.GameFundTransferQueryOperatorResultViewModel> GameFundTransferQueryOperatorAsync(string playerCode, DateTimeOffset startTime, DateTimeOffset endTime, Common.Enums.GameFundTransferSort sort = Common.Enums.GameFundTransferSort.CreatedOn, bool isAscending = true, int page = 1, int rows = 20, CancellationToken cancellationToken = default);

	public ValueTask<Common.ViewModels.GameFundTransferInfoViewModel?> GameFundTransferGetAsync(Guid id, CancellationToken cancellationToken = default);

	public ValueTask<Common.ViewModels.GameFundTransferInfoViewModel> GameFundTransferAddFundInAsync(Guid id, Guid gameProviderId, Guid walletId, string playerCode, string walletFrom, string walletTo, string cashTransactionId, string gameTransactionId, decimal amount, CancellationToken cancellationToken = default);

	public ValueTask<Common.ViewModels.GameFundTransferInfoViewModel> GameFundTransferAddFundOutAsync(Guid id, Guid gameProviderId, Guid walletId, string playerCode, string walletFrom, string walletTo, string cashTransactionId, string gameTransactionId, decimal amount, CancellationToken cancellationToken = default);

	public ValueTask GameFundTransferUpdateByGameWalletFailedAsync(Guid id, string description, JsonNode additionalData, CancellationToken cancellationToken = default);

	public ValueTask GameFundTransferUpdateByMainWalletFailedAsync(Guid id, string description, JsonNode additionalData, CancellationToken cancellationToken = default);

	public ValueTask GameFundTransferUpdateByMainWalletRefundFailedAsync(Guid id, string description, JsonNode additionalData, CancellationToken cancellationToken = default);

	public ValueTask GameFundTransferUpdateByMainWalletRefundSuccessAsync(Guid id, Guid cashRespTransactionId, string gameRespTransactionId, decimal mainWalletBalance, string description, JsonNode additionalData, CancellationToken cancellationToken = default);

	public ValueTask GameFundTransferUpdateBySuccessAsync(Guid id, Guid cashRespTransactionId, string gameRespTransactionId, decimal mainWalletBalance, string description, JsonNode additionalData, CancellationToken cancellationToken = default);
}
