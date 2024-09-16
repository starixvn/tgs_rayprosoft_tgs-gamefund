using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tgs.GameFund.Common.ViewModels;

public record class GameFundTransferTraceBrandQueryViewModel
{
	[Required]
	[DefaultValue(typeof(string), "provider01")]
	public string GameProviderCode { get; init; } = null!;

	[Required]
	[DefaultValue(typeof(string), "brand01")]
	public string BrandCode { get; init; } = null!;
}
