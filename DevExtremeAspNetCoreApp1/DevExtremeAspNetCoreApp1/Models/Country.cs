namespace DevExtremeAspNetCoreApp1.Models
{
    public class Country
    {
        public Name name { get; set; }
        public bool unMember { get; set;}
        //public List<string> languages { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        //public List<CurrencyList> currencies { get; set; }
        //public List<string> coatOfArms { get; set; }
        //public List<string> flags { get; set; }
        public long population { get; set; }
        public List<string> capital { get; set; }
    }                                                       

    public class Name
    {
        public string common { get; set; }
        public string official { get; set; }

    }






}
