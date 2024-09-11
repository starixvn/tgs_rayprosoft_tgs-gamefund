using System.Text.Json.Nodes;

namespace Tgs.GameFund.Common.ViewModels;
public record GameFundTransferUpdateByMainWalletFailedViewModel
{
	public string Description { get; init; } = default!;
	public JsonNode AdditionalData { get; init; } = default!;
}
