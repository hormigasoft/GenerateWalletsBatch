using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Threading.Tasks;
using Nethereum.Wallet.Model;
using Nethereum.Wallet.Services;

namespace Nethereum.Wallet.UI
{
    public class Wallet
    {
        private readonly IWalletConfigurationService walletConfiguration;
        private readonly ITokenRegistryService tokenRegistryService;
        private readonly IAccountRegistryService accountRegistryService;
        public Wallet(IWalletConfigurationService walletConfiguration,
                        ITokenRegistryService tokenRegistryService,
                        IAccountRegistryService accountRegistryService
            )
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            this.walletConfiguration = walletConfiguration;
            this.tokenRegistryService = tokenRegistryService;
            this.accountRegistryService = accountRegistryService;
        }

        public async Task<decimal> EthBalance(string address)
        {
            decimal balance = -1;

            if (walletConfiguration.IsConfigured())
            {
                EthWalletService wallet = new EthWalletService(walletConfiguration, tokenRegistryService, accountRegistryService);
                balance = Convert.ToDecimal(await wallet.GetEthBalance(address));
            }

            return balance;
        }
        public async Task<long> TokenBalance(ContractToken token, string address)
        {
            long balance = -1;

            if (walletConfiguration.IsConfigured())
            {
                EthWalletService wallet = new EthWalletService(walletConfiguration, tokenRegistryService, accountRegistryService);
                balance = Convert.ToInt64(await wallet.GetTokenBalance(token, address));
            }

            return balance;
        }
    }
}