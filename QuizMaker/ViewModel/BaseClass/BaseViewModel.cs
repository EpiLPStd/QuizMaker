using System.ComponentModel;


namespace QuizMaker.ViewModel.BaseClass
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChange(params string[] namesOfProperty)
        {
            if (PropertyChanged != null)
            {
                foreach (var propertyName in namesOfProperty)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
    }
}
