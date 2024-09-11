using System.Text.Json.Nodes;

namespace Tgs.GameFund.Common.ViewModels;
public record GameFundTransferUpdateByGameWalletFailedViewModel
{
	public string Description { get; init; } = default!;
	public JsonNode AdditionalData { get; init; } = default!;
}
