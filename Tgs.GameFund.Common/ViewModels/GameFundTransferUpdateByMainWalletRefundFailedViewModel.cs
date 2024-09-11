using System.Text.Json.Nodes;

namespace Tgs.GameFund.Common.ViewModels;
public record GameFundTransferUpdateByMainWalletRefundFailedViewModel
{
	public string Description { get; init; } = default!;
	public JsonNode AdditionalData { get; init; } = default!;
}
