using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Core.Models.Enums
{
    public enum LedgerAccountGroupCategory
    {
        ImprestAccounts = 1,                     // Related to GetAllImprestLedgerAccountsForSabhaId
        LoanOfLAAccounts = 2,                    // Related to GetLAbankLoanLeggerAccountForSabhaId
        OtherAccountOpeningBalance = 3,           // Related to GetOtherAccountsOpeningBalancessLeggerAccountForSabhaId
        IndustrialCreditorAccounts = 4,           // Related to GetIndustrialCreditorsLeggerAccountForSabhaId
        IndustrialDebtorAccounts = 5,             // Related to GetIndustrialDebitorsLeggerAccountForSabhaId
        StoresAdvanceAccounts = 6,                // Related to GetStoresAdvanceAccountsLeggerAccountForSabhaId
        PrepayableAccounts = 7,                   // Related to GetPrepayableLeggerAccountForSabhaId
        FixedDepositAccounts = 8,                 // Related to GetFixedDepositLeggerAccountForSabhaId
        ReceivableExchangeAccounts = 9,           // Related to GetReceivableExchangeLeggerAccountForSabhaId
        ReceivableNonExchangeAccounts = 10,       // Related to GetReceivableNonExchangeLeggerAccountForSabhaId
        SingleOpeningBalanceAccounts = 11,        // Related to GetSingleOpeningBalanceLedgerAccount
        FixAssetsAccounts = 12,                   // Related to GetFixAssestsForSabhaId and GetFixAssests2ForSabhaId
        AccountTransferAccounts = 13,             // Related to GetAllAccountTransferLedgerAccountsForSabhaId
        PayrollAccounts = 14,                     // Related to GetPayRollAccountsForSabha
        ShopRentalExpectedIncomeAccounts = 15,    // Related to GetAllShopRentalExpectedIncomeAccountsForSabhaId
        StoreAdvanceAssetsAccounts = 16,          // Related to GetStoreAdvanceAssetsAccountsForSabha
        IndustrialCreditorsDebtorFundingSources = 17, // Related to GetSabhaFundingSourceForIndustrialCreditorsDebtorsTypes
        AssetsDisposalAccounts = 18,              // Related to GetAssetsDisposalAccounts
        AssetsGrantRealizationAccounts = 19,      // Related to GetAssetsGrantRealizationAccounts
        AssetsSaleAccounts = 20                   // Related to GetAssetsSaleAccounts
    }

}
