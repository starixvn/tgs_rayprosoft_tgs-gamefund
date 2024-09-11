using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tgs.GameFund.Common.ViewModels;
public record GameFundTransferQueryPlayerViewModel
{
	[Required]
	[DefaultValue(typeof(DateTimeOffset), "2024-09-11T00:00:00+08:00")]
	public DateTimeOffset StartTime { get; init; }
	[Required]
	[DefaultValue(typeof(DateTimeOffset), "2024-09-12T00:00:00+08:00")]
	public DateTimeOffset EndTime { get; init; }
	[Required]
	public Enums.GameFundTransferSort Sort { get; init; }
	[Required]
	[DefaultValue(typeof(bool), "false")]
	public bool IsAscending { get; init; }
	[Required]
	[DefaultValue(typeof(int), "1")]
	public int Page { get; init; }
	[Required]
	[DefaultValue(typeof(int), "20")]
	public int Rows { get; init; }
}
