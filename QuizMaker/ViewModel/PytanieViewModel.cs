using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker.ViewModel
{
    using BaseClass;
    using System.Collections.ObjectModel;

    class PytanieViewModel : BaseViewModel
    {
        private Model.Pytanie pytaniaQuiz;
        public ObservableCollection<Model.Odpowiedź> Odpowiedzi { get; set; }

        public PytanieViewModel(Model.Pytanie pytanie)
        {
            pytaniaQuiz = pytanie;
            DodajOdpowiedzi();
            TreśćPytania = pytanie.Treść;
        }

        public void DodajOdpowiedzi()
        {
            Odpowiedzi = new ObservableCollection<Model.Odpowiedź>();

            foreach (var odpowiedź in pytaniaQuiz.Odpowiedzi) Odpowiedzi.Add(odpowiedź);

            onPropertyChange(nameof(Odpowiedzi));
        }

        private string treśćPytania;
        public string TreśćPytania
        {
            get { return treśćPytania; }
            set
            {
                treśćPytania = value;
                pytaniaQuiz.Treść = value;
                onPropertyChange(nameof(TreśćPytania));
            }
        }
    }
}
