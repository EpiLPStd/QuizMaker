using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker.Model
{
    class Quiz
    {
        public string Nazwa { get; private set; }
        public List<Pytanie> Pytania { get; private set; }

        public Quiz(string nazwa)
        {
            Nazwa = nazwa;
            Pytania = new List<Pytanie>();
        }

        public void AddQuestion(Pytanie pytanie) { Pytania.Add(pytanie); }
        public void SetName(string nazwa) { Nazwa = nazwa; }

        public static Quiz ZPliku(string ścieżka)
        {
            if (!File.Exists(ścieżka)) throw new Exception("Nie znaleziono pliku we wskazanym miejscu.");

            string[] quizTekst = File.ReadAllLines(ścieżka);

            if (quizTekst.Length < 1 || (quizTekst.Length - 1) % 6 != 0) throw new Exception("Nieprawidłowy format zapisu quizu.");

            var QuizNowy = new Quiz(quizTekst[0]);

            if (quizTekst.Length < 7) return QuizNowy;

            for (int i = 1; i < (quizTekst.Length - 1); i += 6)
            {
                var odpowiedzi = new Odpowiedź[4];

                for (int j = 0; j < 4; j++)
                {
                    var odpowiedźPlik = quizTekst[i + j + 1];

                    if (odpowiedźPlik[1] != '|') throw new Exception("Nieprawidłowy format zapisu quizu.");

                    var czyPrawidłowa = odpowiedźPlik[0] == '1';
                    var nazwa = odpowiedźPlik.Substring(2);

                    odpowiedzi[j] = new Odpowiedź(czyPrawidłowa, nazwa);
                }

                var pytanie = new Pytanie(odpowiedzi, quizTekst[i]);
                QuizNowy.AddQuestion(pytanie);
            }

            return QuizNowy;
        }

        public void DoPliku(string ścieżka)
        {
            if (!File.Exists(ścieżka)) File.Create("Quiz Maker - Quiz.txt");

            string[] tekstQuiz = new string[1 + Pytania.Count * 6];
            tekstQuiz[0] = Nazwa;
            int indeks = 1;

            foreach (var pytanie in Pytania)
            {
                tekstQuiz[indeks] = pytanie.Treść;
                indeks++;

                foreach (var odpowiedź in pytanie.Odpowiedzi)
                {
                    int czyPrawidłowa = odpowiedź.CzyPrawidłowa ? 1 : 0;
                    tekstQuiz[indeks] = $"{czyPrawidłowa}|{odpowiedź.Treść}";
                    indeks++;
                }

                tekstQuiz[indeks] = "**********";
                indeks++;
            }

            File.WriteAllLines(ścieżka, tekstQuiz);
        }
    }
}
