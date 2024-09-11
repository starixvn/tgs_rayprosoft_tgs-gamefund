using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tgs.GameFund.Common.ViewModels;
public record GameFundTransferQueryOperatorViewModel
{
	[Required]
	[DefaultValue(typeof(string), "4ucmqplxb029")]
	public string PlayerCode { get; init; } = default!;
	[Required]
	[DefaultValue(typeof(DateTimeOffset), "2024-09-09T00:00:00+08:00")]
	public DateTimeOffset StartTime { get; init; }
	[Required]
	[DefaultValue(typeof(DateTimeOffset), "2024-09-10T00:00:00+08:00")]
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
