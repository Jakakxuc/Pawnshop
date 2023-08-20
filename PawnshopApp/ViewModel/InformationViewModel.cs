using DevExpress.Mvvm;
using Newtonsoft.Json.Linq;
using PawnshopApp.Entities;
using PawnshopApp.Services.BusinessLogic.Interfaces;
using PawnshopApp.Services.Interfaces;
using PawnshopApp.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PawnshopApp.ViewModel
{
    public class InformationViewModel : ViewModelBase
    {
        private Info _info;
        private readonly DispatcherTimer _timer;

        private static readonly string _dbConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/PawnshopSettings.json");

        public Info InfoValue
        {
            get => _info;
            set
            {
                _info = value;
                RaisePropertiesChanged(nameof(InfoValue));
            }
        }

        private readonly ILoanService _loanService;
        private readonly IPawnedItemService _pawnedItemService;

        public InformationViewModel()
        {
            _loanService = ServiceProviderContainer.GetService<ILoanService>();
            _pawnedItemService = ServiceProviderContainer.GetService<IPawnedItemService>();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();

            CalculateStatistics();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CalculateStatistics();
        }

        private void CalculateStatistics()
        {
            string jsonConfig = File.ReadAllText(_dbConfigPath);
            var jObject = JObject.Parse(jsonConfig);

            int initialAmount = jObject["Settings"]["InitialAmount"].Value<int>();

            Info info = new Info();

            decimal loamAmount = _loanService.GetAll().Where(l => l.Status == LoanStatus.Active).Sum(l => l.LoanAmount);

            List<PawnedItem> pawnedItems = _pawnedItemService.GetAll().ToList();
            decimal soldAmount = pawnedItems.Where(pi => pi.IsSold == true).Sum(l => l.EstimatedValue);

            info.InitialAmount = initialAmount;
            info.LoanAmount = loamAmount;
            info.CurrentAmount = initialAmount - loamAmount + soldAmount;
            info.SoldItems = pawnedItems.Count(pi => pi.IsSold == true);
            info.PawnedItemsCount = pawnedItems.Count(pi => pi.IsSold == false);
            info.SoldProfitAmount = soldAmount;

            InfoValue = info;
        }
    }
}
