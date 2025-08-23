/*-----------------------------------------------------------------------
// <copyright file="StockTraderViewModelFactory.cs">
//     Copyright (c) 2025 by Man Tran. All rights reserved.
// </copyright>
// <summary>
//     This file contains the definition of the StockTraderViewModelFactory class, 
//     which provides functionality for data processing.
// </summary>
// History:
// Date         Author             Description
// 2025-08-22   Man Tran           Created the StockTraderViewModelFactory class.
//-----------------------------------------------------------------------*/

using StockTrader.Main.State.Navigators;

namespace StockTrader.Main.VVM.ViewModels.Factories
{
    public class StockTraderViewModelFactory : IStockTraderViewModelFactory
    {
        // Delegates to create a ViewModel according to the type
        private readonly CreateViewModel<HomeViewModel> _createByHomeViewModelDelegate;
        private readonly CreateViewModel<PortfolioViewModel> _createByPortfolioViewModelDelegate;
        private readonly CreateViewModel<LoginViewModel> _createLoginByViewModelDelegate;
        private readonly CreateViewModel<BuyViewModel> _createByBuyViewModelDelegate;
        private readonly CreateViewModel<SellViewModel> _createBySellViewModelDelegate;

        /// <summary>
        /// StockTraderViewModelFactory constructor
        /// </summary>
        /// <param name="createByHomeViewModelDelegate"></param>
        /// <param name="createByPortfolioViewModelDelegate"></param>
        /// <param name="createLoginByViewModelDelegate"></param>
        /// <param name="createByBuyViewModelDelegate"></param>
        /// <param name="createBySellViewModel"></param>
        public StockTraderViewModelFactory(CreateViewModel<HomeViewModel> createByHomeViewModelDelegate, 
            CreateViewModel<PortfolioViewModel> createByPortfolioViewModelDelegate,
            CreateViewModel<LoginViewModel> createLoginByViewModelDelegate,
            CreateViewModel<BuyViewModel> createByBuyViewModelDelegate,
            CreateViewModel<SellViewModel> createBySellViewModel)
        {
            _createByHomeViewModelDelegate = createByHomeViewModelDelegate;
            _createByPortfolioViewModelDelegate = createByPortfolioViewModelDelegate;
            _createLoginByViewModelDelegate = createLoginByViewModelDelegate;
            _createByBuyViewModelDelegate = createByBuyViewModelDelegate;
            _createBySellViewModelDelegate = createBySellViewModel;
        }


        /// <summary>
        /// Method to create a ViewModel based on the ViewType
        /// </summary>
        /// <param name="viewType"> ViewType </param>
        /// <returns> Cuurent ViewModel </returns>
        /// <exception cref="Exception"></exception>
        public BaseViewModel CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return _createLoginByViewModelDelegate();
                case ViewType.Home:
                    return _createByHomeViewModelDelegate();
                case ViewType.Portfolio:
                    return _createByPortfolioViewModelDelegate();
                case ViewType.Buy:
                    return _createByBuyViewModelDelegate();
                case ViewType.Sell:
                    return _createBySellViewModelDelegate();
                default:
                    throw new Exception("View type does not have a ViewModel in the current version.");
            }
        }
    }
}
