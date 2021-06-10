using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizMaker.ViewModel
{
    using BaseClass;
    using System.Diagnostics;
    using System.Windows;
    using Model;
    using System.Collections.ObjectModel;
    using System.Windows.Forms;

    class MainViewModel : BaseViewModel
    {
        private Quiz QuizNowy;
        public ObservableCollection<PytanieViewModel> Pytania { get; set; }

        public MainViewModel()
        {
            Pytania = new ObservableCollection<PytanieViewModel>();
            NowyQuiz();
            Start();
        }

        private Visibility widokStart;
        public Visibility WidokStart
        {
            get { return widokStart; }
            private set
            {
                widokStart = value;
                onPropertyChange(nameof(WidokStart));
            }
        }

        private Visibility widokQuiz;
        public Visibility WidokQuiz
        {
            get { return widokQuiz; }
            private set
            {
                widokQuiz = value;
                onPropertyChange(nameof(WidokQuiz));
            }
        }

        private string nazwaQuizu;
        public string NazwaQuizu
        {
            get { return nazwaQuizu; }
            set
            {
                nazwaQuizu = value;
                QuizNowy.SetName(value);
                onPropertyChange(nameof(NazwaQuizu));
            }
        }

        private ICommand utwórzNowy;
        public ICommand UtwórzNowy
        {
            get
            {
                return utwórzNowy ?? (utwórzNowy = new RelayCommand(QuizUtwórz, obiekt => true));
            }
        }

        private ICommand zPliku;
        public ICommand ZPliku
        {
            get
            {
                return zPliku ?? (zPliku = new RelayCommand(OtwórzPlik, obiekt => true));
            }
        }

        private ICommand dodajPytanie;
        public ICommand DodajPytanie
        {
            get
            {
                return dodajPytanie ?? (dodajPytanie = new RelayCommand(DodajNowePytanie, obiekt => true));
            }
        }


        private ICommand zapiszDoPliku;
        public ICommand ZapiszDoPliku
        {
            get
            {
                return zapiszDoPliku ?? (zapiszDoPliku = new RelayCommand(DoPliku, obiekt => true));
            }
        }

        private void NowyQuiz()
        {
            var odpowiedzi = new Odpowiedź[] { 
                new Odpowiedź(true, "TAK"), 
                new Odpowiedź(false, "NIE"), 
                new Odpowiedź(false, "NIE"), 
                new Odpowiedź(false, "NIE") };

            var pytanie = new Pytanie(odpowiedzi, "Treść pytania");
            var widokPytanie = new PytanieViewModel(pytanie);

            QuizNowy = new Quiz("Nazwa quizu.");
            Pytania.Add(widokPytanie);
            QuizNowy.AddQuestion(pytanie);
            NazwaQuizu = QuizNowy.Nazwa;
        }

        private void QuizUtwórz(object obiekt) { KontrolkaQuiz(); }

        private void OtwórzPlik(object obiekt)
        {
            Microsoft.Win32.OpenFileDialog okno = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "QuizMaker - Quiz",
                DefaultExt = ".txt",
                Filter = "Dokument tekstowy (.txt)|*.txt"
            };

            bool? czyOkno = okno.ShowDialog();

            if (czyOkno == true)
            {
                string filename = okno.FileName;
                QuizNowy = Quiz.ZPliku(filename);
                NazwaQuizu = QuizNowy.Nazwa;
                Pytania.Remove(Pytania[0]);

                foreach (var pytanie in QuizNowy.Pytania) Pytania.Add(new PytanieViewModel(pytanie));
            }
            else return;

            KontrolkaQuiz();
        }

        private void DodajNowePytanie(object obiekt)
        {
            var odpowiedzi = new Odpowiedź[] {
                new Odpowiedź(true, "TAK"),
                new Odpowiedź(false, "NIE"),
                new Odpowiedź(false, "NIE"),
                new Odpowiedź(false, "NIE") };

            var pytanie = new Pytanie(odpowiedzi, "Treść pytania");
            var widokPytanie = new PytanieViewModel(pytanie);

            QuizNowy.AddQuestion(pytanie);
            Pytania.Add(widokPytanie);
        }

        private void DoPliku(object obiekt)
        {
            var okno = new Microsoft.Win32.SaveFileDialog { Filter = "Dokument tekstowy (*.txt)|*.txt" };
            var czyOkno = okno.ShowDialog();

            if (czyOkno == true) QuizNowy.DoPliku(okno.FileName);
        }
          
        private void Start()
        {
            WidokStart = Visibility.Visible;
            WidokQuiz = Visibility.Hidden;
        }

        private void KontrolkaQuiz()
        {
            WidokStart = Visibility.Hidden;
            WidokQuiz = Visibility.Visible;
        }
    }
}
