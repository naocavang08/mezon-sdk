using Mezon_sdk.Mmn.Models;
using System.Numerics;

namespace Mezon_sdk.Mmn.Utils
{
    public static class ProtoConverter
    {
        public static BigInteger Uint256FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return BigInteger.Zero;

            return BigInteger.Parse(value);
        }

        public static string Uint256ToString(BigInteger value)
        {
            return value.ToString();
        }

        public static global::Mmn.TxMsg ToProtoTx(Tx tx)
        {
            return new global::Mmn.TxMsg
            {
                Type = tx.Type,
                Sender = tx.Sender,
                Recipient = tx.Recipient,
                Amount = Uint256ToString(tx.Amount),
                Nonce = tx.Nonce,
                TextData = tx.TextData,
                Timestamp = tx.Timestamp,
                ExtraInfo = tx.ExtraInfo,
                ZkProof = tx.ZkProof,
                ZkPub = tx.ZkPub
            };
        }

        public static global::Mmn.SignedTxMsg ToProtoSigTx(SignedTx tx)
        {
            return new global::Mmn.SignedTxMsg
            {
                TxMsg = ToProtoTx(tx.Tx),
                Signature = tx.Sig
            };
        }

        public static Account FromProtoAccount(global::Mmn.GetAccountResponse acc)
        {
            return new Account
            {
                Address = acc.Address,
                Balance = Uint256FromString(acc.Balance),
                Nonce = acc.Nonce
            };
        }

        public static TxHistoryResponse FromProtoTxHistory(global::Mmn.GetTxHistoryResponse res)
        {
            var txs = new List<TxInfo>();
            foreach (var tx in res.Txs)
            {
                txs.Add(new TxInfo
                {
                    Sender = tx.Sender,
                    Recipient = tx.Recipient,
                    Amount = Uint256FromString(tx.Amount),
                    Nonce = tx.Nonce,
                    Timestamp = (long)tx.Timestamp,
                    ExtraInfo = tx.ExtraInfo,
                    Status = tx.Status.ToString()
                });
            }

            return new TxHistoryResponse
            {
                Total = res.Total,
                Txs = res.Txs.Select(tx => new TxMetaResponse
                {
                    Sender = tx.Sender,
                    Recipient = tx.Recipient,
                    Amount = Uint256FromString(tx.Amount),
                    Nonce = tx.Nonce,
                    Timestamp = tx.Timestamp,
                    Status = (TxMetaStatus)tx.Status
                }).ToList(),
                Transactions = txs
            };
        }
    }
}
