using System;
using System.Linq;

namespace QuizMaker.Model
{
    class Pytanie
    {
        public Odpowiedź[] Odpowiedzi = new Odpowiedź[4];
        public string Treść { get; set; }

        public Pytanie(Odpowiedź[] odpowiedzi, string treść)
        {
            if (!FormatOdpowiedzi(odpowiedzi)) throw new Exception("Nieprawidłowy format odpowiedzi.");

            Odpowiedzi = odpowiedzi;
            Treść = treść;
        }

        private static Random random = new Random();
        public static string LosoweId()
        {
            const string znaki = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(znaki, 8).Select(znak => znak[random.Next(znak.Length)]).ToArray());
        }

        public static bool FormatOdpowiedzi(Odpowiedź[] odpowiedzi)
        {
            if (odpowiedzi.Length > 4) return false;

            string id = LosoweId();
            int prawidłowe = 0;

            foreach (var odpowiedź in odpowiedzi)
            {
                if (odpowiedź.CzyPrawidłowa) prawidłowe++;
                odpowiedź.Id = id;
            }

            if (prawidłowe != 1) return false;

            return true;
        }
    }
}
