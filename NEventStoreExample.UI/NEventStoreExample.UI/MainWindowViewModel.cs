using MemBus;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Input;
using System;
using NEventStoreExample.Command;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NEventStoreExample.UI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IBus Bus;

        public ICommand UpdateAccountDetails { get; set; }
        public ICommand WithdrawAmount { get; set; }
        public ICommand DepositAmount { get; set; }
        public ICommand CreateAccount { get; set; }
        public ICommand CloseAccount { get; set; }

        public MainWindowViewModel(IBus bus)
        {
            Bus = bus;
            LoadActiveAccountsFromDatabase();

            CreateAccount = new RelayCommand(PublishCreateAccountCommand);
            UpdateAccountDetails = new RelayCommand(PublishUpdateAccountDetailsCommand);
            WithdrawAmount = new RelayCommand(PublishWithdrawMoneyCommand);
            DepositAmount = new RelayCommand(PublishDepositMoneyCommand);
            CloseAccount = new RelayCommand(PublishCloseAccountCommand);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void LoadActiveAccountsFromDatabase()
        {
            var accounts = new List<AccountDisplay>();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EventStoreExampleConnection"].ConnectionString))
            using (var command = new SqlCommand("SELECT ID, Name, Amount, Address, City FROM ActiveAccounts", connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var account = new AccountDisplay();
                    account.ID = reader.GetGuid(0);
                    account.Name = reader[1] as string;
                    account.Amount = reader[2] as string;
                    account.Address = reader[3] as string;
                    account.City = reader[4] as string;
                    accounts.Add(account);
                }
            }
            ActiveAccounts = accounts;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PublishCreateAccountCommand()
        {
            Bus.Publish(new CreateAccountCommand(Guid.NewGuid(), AccountHolderName));
            LoadActiveAccountsFromDatabase();
        }

        private void PublishCloseAccountCommand()
        {
            if (CurrentAccount != null)
                Bus.Publish(new CloseAccountCommand(CurrentAccount.ID));
        }

        private void PublishUpdateAccountDetailsCommand()
        {
            if (CurrentAccount != null)
                Bus.Publish(new SetAccountDetailsCommand(CurrentAccount.ID, "", CurrentAccount.Address, CurrentAccount.City));
        }

        private void PublishDepositMoneyCommand()
        {
            if (CurrentAccount != null)
            {
                Bus.Publish(new DepositMoneyCommand(CurrentAccount.ID, Amount));
                LoadActiveAccountsFromDatabase();
            }
        }

        private void PublishWithdrawMoneyCommand()
        {
            if (CurrentAccount != null)
            {
                Bus.Publish(new WithdrawMoneyCommand(CurrentAccount.ID, Amount));
                LoadActiveAccountsFromDatabase();
            }
        }

        public string AccountHolderName { get; set; }
        public double Amount { get; set; }

        private IEnumerable<AccountDisplay> activeAccounts;
        public IEnumerable<AccountDisplay> ActiveAccounts
        {
            get
            {
                return activeAccounts;
            }
            set
            {
                activeAccounts = value;
                NotifyPropertyChanged();
            }
        }

        private AccountDisplay currentAccount;
        public AccountDisplay CurrentAccount
        {
            get
            {
                return currentAccount;
            }
            set
            {
                if (currentAccount != value)
                {
                    currentAccount = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}