using System.Text.Json.Nodes;

namespace Tgs.GameFund.Common.ViewModels;
public record GameFundTransferUpdateBySuccessViewModel
{
	public Guid CashRespTransactionId { get; init; }
	public string GameRespTransactionId { get; init; } = default!;
	public decimal MainWalletBalance { get; init; }
	public string Description { get; init; } = default!;
	public JsonNode AdditionalData { get; init; } = default!;
}
