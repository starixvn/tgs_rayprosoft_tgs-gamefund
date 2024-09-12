using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tgs.GameFund.Common.ViewModels;

public record class GameFundTransferTraceSaveViewModel
{
	[Required]
	[DefaultValue(typeof(Guid), "00000000-0000-0000-0000-000000000000")]
	public Guid GameProviderId { get; init; }

	[Required]
	[DefaultValue(typeof(Guid), "00000000-0000-0000-0000-000000000000")]
	public Guid WalletId { get; init; }

	[Required]
	[DefaultValue(typeof(string), "4ucmqplxb029")]
	public string PlayerCode { get; init; } = default!;
}
