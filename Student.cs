
namespace StudentsDiary
{
    public class Student
    {
        /*
        private string _property;
        public string Property
        {
            get
            {
                if (_property == "1")
                    throw new Exception("Zła wartość.");
                return _property;
            }
            set
            {
                System.Windows.Forms.MessageBox.Show("Przypisywanie wartości - SET");
                _property = value;
            }
        }

        public string PublicField;
        private string _privateField;
        */
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }
        public string Math { get; set; }
        public string Technology { get; set; }
        public string Physics { get; set; }
        public string PolishLang { get; set; }
        public string ForeingLang { get; set; }
        public bool Activities { get; set; }
        public int IdGroup { get; set; }
    }

}
