using System.Numerics;

namespace Mezon_sdk.Mmn.Models
{
    /// <summary>
    /// Represents an account
    /// </summary>
    public class Account
    {
        public string Address { get; set; } = string.Empty;
        public BigInteger Balance { get; set; }
        public ulong Nonce { get; set; }
    }
}
