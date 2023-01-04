namespace Core.Resources.Dictionary
{
    public static class CustomDictionary
    {
        public static Dictionary<string, string> _cd = new Dictionary<string, string>();

        public static void Initialise()
        {
            _cd.Add("Vacation", "Məzuniyyət Müraciəti");
            _cd.Add("Education", "Təhsil Müraciəti"); 
            _cd.Add("NonPaid", "Ödənişsiz İzin Müraciəti"); 
            _cd.Add("HalfPaid", "Qismən ödənişli İzin");

            _cd.Add("New", "Yeni");
            _cd.Add("SentToManager", "Rəhbərə Göndərildi");
            _cd.Add("ManagerDeclined", "Rəhbər Rədd Etdi");
            _cd.Add("SentToHr", "İnsan resurslarına göndərildi");
            _cd.Add("Approved", "Qeydə alındı");
            _cd.Add("Declined", "İmtina edildi");
        }

        public static Dictionary<string, string> GetResource()
        {
            return _cd;
        }
    }
}
