using BasquetballCount.CustomControlls.CustomDialogs.Dialogs;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BasquetballCount.CustomControlls.CustomDialogs.Services.Imp
{
    public class DialogService : IDialogService
    {
        private readonly IPopupNavigation _popupNavigation;
        public DialogService()
        {
            _popupNavigation = PopupNavigation.Instance;
        }
        protected Page CurrentMainPage => Application.Current.MainPage;
        public async Task ShowDialogAsync(string title, string message, string buttonText)
        {
            await CurrentMainPage.DisplayAlert(title, message, buttonText);
        }
        #region Custom Popups
        public async Task<bool> ShowConfirmationDialogAsync(string title, string message, string yesButtonText = "Si", string noButtonText = "No")
        {
            var confirmationDialog = new ConfirmationDialog(title, message)
            {
                YesButtonText = yesButtonText,
                NoButtonText = noButtonText
            };
            await _popupNavigation.PushAsync(confirmationDialog);
            var result = await confirmationDialog.GetResult();
            await _popupNavigation.PopAllAsync();
            return (bool)result;
        }
        //public async Task<bool> ShowConfirmationDialogAsyncTwo(string title, string message, string yesButtonText = "Yes", string noButtonText = "No")
        //{
        //    var confirmationDialog = new ConfirmationDialog(title, message)
        //    {
        //        YesButtonText = yesButtonText,
        //        NoButtonText = noButtonText
        //    };
        //    await _popupNavigation.PushAsync(confirmationDialog);
        //    var result = await confirmationDialog.GetResult();
        //    await _popupNavigation.PopAsync();
        //    return (bool)result;
        //}
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task ShowInformationDialogAsync(string title, string message)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            //var informatinoDialog = new InfoDialog(title, message);
            //await _popupNavigation.PushAsync(informatinoDialog);
        }
        public async Task CloseOpendDialogsAsync()
        {
            await _popupNavigation.PopAllAsync();
        }
        public async Task ShowOkDialogAsync(string title, string message)
        {
            //var okDialog = new OkDialog(title, message);
            //await _popupNavigation.PushAsync(okDialog);
        }
        public async Task ShowErrorDialogAsync(string title, string message)
        {
            //var okDialog = new OkDialog(title, message);
            //await _popupNavigation.PushAsync(okDialog);
        }
        #endregion Custom Popups
    }
}
