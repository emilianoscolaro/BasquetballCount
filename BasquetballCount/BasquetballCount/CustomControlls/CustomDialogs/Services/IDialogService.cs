using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasquetballCount.CustomControlls.CustomDialogs.Services
{
    public interface IDialogService
    {
        Task ShowDialogAsync(string title, string message, string buttonText);
        Task ShowInformationDialogAsync(string title, string message);
        Task ShowOkDialogAsync(string title, string message);
        Task<bool> ShowConfirmationDialogAsync(string title, string message, string yesButtonText = "Yes", string noButtonText = "No");
        Task ShowErrorDialogAsync(string title, string message);
        Task CloseOpendDialogsAsync();
    }
}
