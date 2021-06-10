namespace QuizMaker.Model
{
    class Odpowiedź
    {
        public string Id { get; set; }
        public bool CzyPrawidłowa { get; set; }
        public string Treść { get; set; }

        public Odpowiedź(bool czyPrawidłowa, string treść)
        {
            CzyPrawidłowa = czyPrawidłowa;
            Treść = treść;
            Id = "odpowiedź";
        }
    }
}
