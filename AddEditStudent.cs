using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class AddEditStudent : Form
    {
        // private string _filePath = Path.Combine(Environment.CurrentDirectory, "students.txt");

        private FileHelper<List<Student>> _fileHelper =
            new FileHelper<List<Student>>(Program.FilePath);

        private int _studentId;
        private Student _student;
        public AddEditStudent(int id = 0)
        {
            InitializeComponent();
            _studentId = id;

            GetStudentData();
            tbFirstName.Select();
        }

        private void GetStudentData()
        {
            if (_studentId != 0)
            {
                Text = "Edytowanie danych ucznia";
                var students = _fileHelper.DeserializerFormFile();
                _student = students.FirstOrDefault(x => x.Id == _studentId);

                if (_student == null) throw new Exception("Brak użytkownika o podanym ID");

                FillTextBoxes();
            }
        }

        private void FillTextBoxes()
        {
            tbId.Text = _student.Id.ToString();
            tbFirstName.Text = _student.FirstName;
            tbLastName.Text = _student.LastName;
            tbMath.Text = _student.Math;
            tbPhysics.Text = _student.Physics;
            tbTechnology.Text = _student.Technology;
            tbPolishLang.Text = _student.PolishLang;
            tbForeingLang.Text = _student.ForeingLang;
            rtbComments.Text = _student.Comments;
            cbActivities.Checked = _student.Activities;
            cbbIdGroup.SelectedIndex = _student.IdGroup;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializerFormFile();

            if (_studentId != 0) students.RemoveAll(x => x.Id == _studentId);

            else AssignIdToNewStudent(students);

            AddNewUserToList(students);

            _fileHelper.SerializeToFile(students);
            await LongprocessAsync();
            Close();
        }


        private async Task LongprocessAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(10);
            });
        }

        private void AddNewUserToList(List<Student> students)
        {
            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Comments = rtbComments.Text,
                ForeingLang = tbForeingLang.Text,
                Math = tbMath.Text,
                Physics = tbPhysics.Text,
                PolishLang = tbPolishLang.Text,
                Technology = tbTechnology.Text,
                Activities = cbActivities.Checked,
                IdGroup = cbbIdGroup.SelectedIndex
            };

            students.Add(student);
        }
        private void AssignIdToNewStudent(List<Student> students)
        {
            var studentWithHigestId = students.OrderByDescending(x => x.Id).FirstOrDefault();
            /*
            var studentId = 0;

            if(studentWithHigestId == null)
            {
                studentId = 1;
            }
            else
            {
                studentId = studentWithHigestId.Id + 1;
            }
            */
            _studentId = studentWithHigestId == null ? 1 : studentWithHigestId.Id + 1;
        }
    }
}
