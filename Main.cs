using StudentsDiary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class Main : Form
    {


        private FileHelper<List<Student>> _fileHelper =
            new FileHelper<List<Student>>(Program.FilePath);

        public bool IsMaximize
        {
            get
            {
                return Settings.Default.IsMaximize;
            }
            set
            {
                Settings.Default.IsMaximize = value;
            }

        }

        public Main()
        {
            InitializeComponent();
            RefreshDiary();
            SetColumnsHeader();

            if (IsMaximize) WindowState = FormWindowState.Maximized;
        }

        private void RefreshDiary()
        {
            var students = _fileHelper.DeserializerFormFile();
            dgvDiary.DataSource = students;
        }

        private void RefreshDiary(int selectedIndex)
        {
            if(selectedIndex == 3)
            {
                var students = _fileHelper.DeserializerFormFile();
                dgvDiary.DataSource = students;
            }
            else
            {
                var students = _fileHelper.DeserializerFormFile();
                var studentsOrdered = students.Where(x => x.IdGroup == selectedIndex).ToList();
                dgvDiary.DataSource = studentsOrdered;
            }
        }

        private void SetColumnsHeader()
        {
            dgvDiary.Columns[0].HeaderText = "Numer";
            dgvDiary.Columns[1].HeaderText = "Imię";
            dgvDiary.Columns[2].HeaderText = "Nazwisko";
            dgvDiary.Columns[3].HeaderText = "Uwagi";
            dgvDiary.Columns[4].HeaderText = "Matematyka";
            dgvDiary.Columns[5].HeaderText = "Technologia";
            dgvDiary.Columns[6].HeaderText = "Fizyka";
            dgvDiary.Columns[7].HeaderText = "Język polski";
            dgvDiary.Columns[8].HeaderText = "Język obcy";
            dgvDiary.Columns[9].HeaderText = "Zaj. Dodatkowe";
            dgvDiary.Columns[9].ReadOnly = true;
            dgvDiary.Columns[10].Visible = false;
        }


        

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary(cbxGroup.SelectedIndex);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznacz ucznia, którego dane chcesz usunąć.");
                return;
            }

            var selectedStudent = dgvDiary.SelectedRows[0];

            var confirmDelete = 
                MessageBox.Show($"Czy na pewno chcesz usunąć ucznia" +
                $" {(selectedStudent.Cells[1].Value.ToString() + " " + selectedStudent.Cells[2].Value.ToString()).Trim()}", 
                "Usuwanie Ucznia", 
                MessageBoxButtons.OKCancel);

            if(confirmDelete == DialogResult.OK)
            {
                DeleteStudent(Convert.ToInt32(selectedStudent.Cells[0].Value));
                RefreshDiary();
            }
        }

        private void DeleteStudent(int id)
        {
            var students = _fileHelper.DeserializerFormFile();
            students.RemoveAll(x => x.Id == id);
            _fileHelper.SerializeToFile(students);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznacz ucznia, którego dane chcesz edytować.");
                return;
            }

            var addEditStudent = new AddEditStudent(Convert.ToInt32(dgvDiary.SelectedRows[0].Cells[0].Value));
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudent();
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog();
        }

        private void AddEditStudent_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshDiary();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(WindowState == FormWindowState.Maximized) IsMaximize = true;
            
            else IsMaximize = false;

            Settings.Default.Save();
        }
    }
}

//private string _filePath = $@"{Environment.CurrentDirectory}\students.txt";
//private string _filePath = Path.Combine(Environment.CurrentDirectory, "students.txt");

/* 
 *         public void DisplayMessage1(string message)
        {
            MessageBox.Show($"Metoda 1 - {message}");
        }

        public void DisplayMessage2(string message)
        {
            MessageBox.Show($"Metoda 2 - {message}");
        }

 * var student = new Student();
student.Address.City = "1";
try
{
    var student = new Student();
    student.Property = "2";
    MessageBox.Show(student.Property);
}
catch (Exception ex)
{
    MessageBox.Show(ex.Message);
}
//student.PublicField = "1";


var list = new List<Person>()
{
    new Student { FirstName = "StudentImie", LastName = "StudentNzwisko", Math = "2" },
    new Teacher { FirstName = "TeacherImie", LastName = "TeacherNzwisko"},
};

foreach(var item in list)
{
    MessageBox.Show(item.GetInfo());
}
var student1 = new Student();
student1.FirstName = "1";
student1.Id = 1;


var person = new Person();
person.Id = 2;

person = student1;

MessageBox.Show(person.Id.ToString());


var list = new List<int> { -2, 432, 22, 5, 85 };

var list2 = (from x in list where x > 10 orderby x descending select x).ToList();


var list3 = list.Where(x => x > 10).OrderByDescending(x => x).ToList();

var students = new List<Student>();
var students1 = from x in students select x.Id;

var students2 = students.Select(x => x.Id);

var allPositives = list.All(x => x > 0);
//MessageBox.Show(allPositives.ToString());

var anyNumberBiggerThan100 = list.Any(x => x > 100);
//MessageBox.Show(anyNumberBiggerThan100.ToString());
var contain10 = list.Contains(10);
var sum = list.Sum();
var count = list.Count();
var avg = list.Average();
var max = list.Max();
var firstElement = list.First(x => x > 10);



var students = DeserializerFormFile();

foreach (var student in students)
{

}


var students = new List<Student>();
students.Add(new Student { FirstName = "Jan" });
students.Add(new Student { FirstName = "Marek" });
students.Add(new Student { FirstName = "Małgosia" });

SerializeToFile(students);

var path = $@"{Path.GetDirectoryName(Application.ExecutablePath)}\..\NowyPlik2.txt";

if (!File.Exists(path))
{
    File.Create(path);
}
//File.WriteAllText(PaddingChanged, "Test");
//File.AppendAllText(PaddingChanged, "Test\n");
//System.IO.File.Create(@"F:\kurs.net\StudentsDiary\NowyPlik.txt");
var text = File.ReadAllText(path);

MessageBox.Show(text);
MessageBox.Show("Test", "Tytuł", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
*/

/*
public void SerializeToFile(List<Student> students)
{
    var serializer = new XmlSerializer(typeof(List<Student>));
    StreamWriter streamWriter = null;
    try
    {

        streamWriter = new StreamWriter(_filePath);
        serializer.Serialize(streamWriter, students);
        streamWriter.Close();
    }
    finally
    {
        streamWriter.Dispose();
    }
}
*/