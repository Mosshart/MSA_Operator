

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace ReturnHomeBtn.ViewModels
{
    /// <summary>
    /// View model of Return button control 
    /// </summary>
    public class ReturnBtnViewModel : BindableBase
    {
        private IEventAggregator _ea;

        /// <summary>
        /// button click action
        /// </summary>
        public DelegateCommand ReturnHomeBtnCommand { get; private set; }
        public ReturnBtnViewModel(IEventAggregator ea)
        {
            _ea = ea;
            ReturnHomeBtnCommand = new DelegateCommand(ReturnHomeBtn);
        }

        private void ReturnHomeBtn()
        {

            //tutaj wyslanie do robota sciezki powrotu do operatora

        }
    }
}
