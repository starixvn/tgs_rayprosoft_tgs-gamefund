using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tgs.GameFund.Common.ViewModels;

public record GameFundTransferAddViewModel
{
	[Required]
	[DefaultValue(typeof(Guid), "00000000-0000-0000-0000-000000000000")]
	public Guid Id { get; init; }
	[Required]
	[DefaultValue(typeof(Guid), "00000000-0000-0000-0000-000000000000")]
	public Guid GameProviderId { get; init; }
	[Required]
	[DefaultValue(typeof(Guid), "00000000-0000-0000-0000-000000000000")]
	public Guid WalletId { get; init; }
	[Required]
	[DefaultValue(typeof(string), "4ucmqplxb029")]
	public string PlayerCode { get; init; } = default!;
	[Required]
	[DefaultValue(typeof(string), "Default")]
	public string WalletFrom { get; init; } = default!;
	[Required]
	[DefaultValue(typeof(string), "Default")]
	public string WalletTo { get; init; } = default!;
	[Required]
	[DefaultValue(typeof(string), "00000000-0000-0000-0000-000000000000")]
	public string CashTransactionId { get; init; } = default!;
	[Required]
	[DefaultValue(typeof(string), "00000000-0000-0000-0000-000000000000")]
	public string GameTransactionId { get; init; } = default!;
	[Required]
	[DefaultValue(typeof(decimal), "0")]
	[Range(minimum: 0, maximum: 10000000.00)]
	public decimal Amount { get; init; }
}
