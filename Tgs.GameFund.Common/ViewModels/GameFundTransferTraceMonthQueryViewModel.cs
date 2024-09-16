using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tgs.GameFund.Common.ViewModels;

public record class GameFundTransferTraceMonthQueryViewModel
{
	[Required]
	[DefaultValue(typeof(string), "provider01")]
	public string GameProviderCode { get; init; } = null!;

	[Required]
	[DefaultValue(typeof(int), "2024")]
	public int Year { get; init; }

	[Required]
	[DefaultValue(typeof(int), "9")]
	public int Month { get; init; }
}
