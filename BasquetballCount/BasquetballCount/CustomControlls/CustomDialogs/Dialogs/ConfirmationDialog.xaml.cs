using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamF.CustomControlls.CustomDialogs.Dialogs.Base;

namespace BasquetballCount.CustomControlls.CustomDialogs.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmationDialog : DialogBase
    {
        public string YesButtonText { get; set; }
        public string NoButtonText { get; set; }
        public ConfirmationDialog(string title, string message)
        {
            InitializeComponent();
            base.Title = title;
            base.Message = message;
            OnApearing = () =>
            {
                this.btnNo.Text = NoButtonText;
                this.btnYes.Text = YesButtonText;
            };
            this.btnNo.Clicked += (sender, args) =>
            {
                if (Proccess.Task.Status.Equals(System.Threading.Tasks.TaskStatus.WaitingForActivation))
                {
                    Proccess.SetResult(false);
                }
            };

            this.btnYes.Clicked += (sender, args) =>
            {
                if (Proccess.Task.Status.Equals(System.Threading.Tasks.TaskStatus.WaitingForActivation))
                {
                    Proccess.SetResult(true);
                }
            };
        }
    }
}