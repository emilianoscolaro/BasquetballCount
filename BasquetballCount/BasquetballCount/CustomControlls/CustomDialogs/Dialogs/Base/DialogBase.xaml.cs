using Acr.UserDialogs;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamF.CustomControlls.CustomDialogs.Dialogs.Base
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogBase : PopupPage
    {
        #region Variables
        //protected Dictionary<DialogAction, Task> DialogActions;
        protected Action OnApearing;
        protected TaskCompletionSource<object> Proccess;
        //protected ILocator _locator;
        protected IPopupNavigation _popupNavigation;
        #endregion Variables
        #region Properties
        protected string _title;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        protected string Title { get => _title; set => _title = value; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        protected string _message;
        protected string Message { get => _message; set => _message = value; }

        public bool IsError { get; set; }

        #endregion Properties
        public DialogBase()
        {
            UserDialogs.Instance.HideLoading();
            InitializeComponent();
            //_locator = App.Locator;
            _popupNavigation = PopupNavigation.Instance;
            this.CloseWhenBackgroundIsClicked = false;
        }
        #region Bindable Properties
        public static readonly BindableProperty ActionsPlaceHolderProperty
            = BindableProperty.Create(nameof(ActionsPlaceHolder), typeof(View), typeof(DialogBase));
        public View ActionsPlaceHolder
        {
            get { return (View)GetValue(ActionsPlaceHolderProperty); }
            set { SetValue(ActionsPlaceHolderProperty, value); }
        }
        #endregion Bindable Properties
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.lblTitle.Text = Title;
            this.lblMessage.Text = Message;
            //lytTitle.BackgroundColor = IsError ? (Color)Application.Current.Resources["ErrorTextColor"] : (Color)Application.Current.Resources["TertiaryColor"];
            OnApearing?.Invoke();
            Proccess = new TaskCompletionSource<object>();
        }
        public virtual Task<object> GetResult()
        {
            return Proccess.Task;
        }
    }
}