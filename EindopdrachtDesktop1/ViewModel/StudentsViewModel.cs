using EindopdrachtDesktop1.Databases;
using EindopdrachtDesktop1.Model;
using MenuNavigatie.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace EindopdrachtDesktop1.ViewModel
{
    internal class StudentsViewModel : ObservableObject
    {
        #region fields
        private Student? _selectedStudent;
        public ObservableCollection<Student> Students { get; }
        private Student? _student = new Student();
        private Course _selectedcourse;
       
        public ObservableCollection<Course> Courses { get; }
        #endregion

        #region properties
        public Student? SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
              
                 _selectedStudent = value;
                OnPropertyChanged();
                SelectedStudentChanged();

            }
        }
        public Student? Student
        {
            get { return _student; }
            set
            {
                _student = value;
                OnPropertyChanged();

            }
        }

        public Course SelectedCourse
        {
            get { return _selectedcourse; }

            set
            {

                _selectedcourse = value;
                Student.Course = value;
                OnPropertyChanged();

            }
        }
        #endregion

        #region Commands
        public ICommand AddStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand UpdateStudentCommand { get; set; }
        public ICommand CanAddStudentCommand { get; set; }
        public ICommand CanUpdateStudentCommand { get; set; }
        public ICommand CanDeleteStudentCommand { get; set; }
        #endregion

        #region constructors
        public StudentsViewModel()
        { 
          
            using (Dbcontext context = new Dbcontext())
            {
                Students = new ObservableCollection<Student>(
                    context.Students.Include(s => s.Course).ToList()
                );
                Courses = new ObservableCollection<Course>(context.Courses.ToList());
            }
            AddStudentCommand = new RelayCommand(ExecuteAddStudent, CanExecuteAddStudent);
            DeleteStudentCommand = new RelayCommand(ExecuteDeleteStudent, CanDeleteStudent);
            UpdateStudentCommand = new RelayCommand(ExecuteUpdateStudent, CanExecuteUpdateStudent);
        }
        #endregion

        #region methods
        // checkt of the student geen null is en valideert de student.
        private bool CanExecuteAddStudent(object? obj)
        {
            using Dbcontext context = new Dbcontext();
            return !string.IsNullOrWhiteSpace(Student.Name)
                   && !string.IsNullOrWhiteSpace(Student.Email)
                   && Student.Number > 0 || Student.Number < sbyte.MaxValue
                   && Student.StudentNumber > 0 && SelectedCourse != null;
        }

        // checks of de selected course niet null is en adds de student naar de database students. 
        private void ExecuteAddStudent(object? obj)
        {
            using Dbcontext context = new Dbcontext();
            if (SelectedCourse != null)
            {
                Student.CourseID = SelectedCourse.Id; 
                context.Students.Add(Student);
                context.SaveChanges();
                Students.Add(Student);
                Student = new Student(); 
            }
            else
            {
                MessageBox.Show("Please select a course before adding a student.");
            }
        }

        // checks of het geen null is en verwijdert de student uit de database.
        private void ExecuteDeleteStudent(object? obj)
        {
            if (_selectedStudent == null) return;

            try
            {
                using (var context = new Dbcontext())
                {
                    context.Students.Remove(_selectedStudent);
                    context.SaveChanges();
                }
                Students.Remove(_selectedStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting student");
            }
        }

        private bool CanDeleteStudent(object? obj)
        {
            return _selectedStudent != null;
        }

        // checkt of de selected student geen null is voor updaten. 
        private bool CanExecuteUpdateStudent(object? obj)
        {
            return _selectedStudent != null;
           
        }
        // checks of de selected student geen null is en update het naar de database. 
        private void ExecuteUpdateStudent(object? obj)
        {

            if (_selectedStudent != null)
            {
                _selectedStudent.Name = Student.Name;
                _selectedStudent.StudentNumber = Student.StudentNumber;
                _selectedStudent.Number = Student.Number;
                _selectedStudent.Email = Student.Email;
                _selectedStudent.StudentNumber = Student.StudentNumber;
                _selectedStudent.CourseID = Student.CourseID;

                using Dbcontext dbcontext = new();
                var context = dbcontext.Students.FirstOrDefault(s => s.Id == _selectedStudent.Id);
                if (context != null)
                {
                    context.Name = Student.Name;
                    context.StudentNumber = Student.StudentNumber;
                    context.Number = Student.Number;
                    context.Email = Student.Email;
                    context.StudentNumber = Student.StudentNumber;
                    context.Course = Student.Course;

                    dbcontext.SaveChanges();
                    Student = new Student();
                }
            }
        }
        // checks of the selected student geen null is en zet de waardes naar de nieuwe student.
        private void SelectedStudentChanged()
        {
            if (SelectedStudent == null)
            {
                Student = new Student();
            }
            else
            {
                Student = new Student
                {
                    Id = SelectedStudent.Id,
                    Name = SelectedStudent.Name,
                    Number = SelectedStudent.Number,
                    Email = SelectedStudent.Email,
                    StudentNumber = SelectedStudent.StudentNumber,
                    Course = SelectedStudent.Course
                };
            }
        }
        #endregion 
    }
}
