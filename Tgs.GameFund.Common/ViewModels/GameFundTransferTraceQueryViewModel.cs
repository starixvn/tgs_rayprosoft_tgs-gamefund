using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tgs.GameFund.Common.ViewModels;

public record class GameFundTransferTraceQueryViewModel
{
	[Required]
	[DefaultValue(typeof(string), "provider01")]
	public string GameProviderCode { get; init; } = default!;

	public string? BrandCode { get; init; }

	public DateTimeOffset? StartTime { get; init; }

	public DateTimeOffset? EndTime { get; init; }
}
