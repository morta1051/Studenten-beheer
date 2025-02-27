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
    class CoursesViewModel : ObservableObject
    {
        #region fields

        private Course? _selectedCourse;
        public ObservableCollection<Course> _courses;
        private Course? _course = new Course();
        public ObservableCollection<Student> Students { get; set; }
        #endregion

        #region properties
        public Course? SelectedCourse
        {
            get { return _selectedCourse; }
            set
            { 
                _selectedCourse = value;
                OnPropertyChanged();
                SelectedCourseChanged();
            }
        }

        public Course? Course
        {
            get { return _course; }
            set
            {
                _course = value;
                OnPropertyChanged();

            }
        }
        public ObservableCollection<Course> Courses
        {
            get { return _courses; }
            set
            {
                _courses = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Commands

        public ICommand AddCourseCommand { get; set; }
        public ICommand DeleteCourseCommand { get; set; }
        public ICommand UpdateCourseCommand { get; set; }
        public ICommand CanAddCourseCommand { get; set; }
        public ICommand CanUpdateCourseCommand { get; set; }
        #endregion

        #region Constructors

        public CoursesViewModel()
        {
            _courses = new ObservableCollection<Course>();
            AddCourseCommand = new RelayCommand(ExecuteAddCourse, CanExecuteAddCourse);
            DeleteCourseCommand = new RelayCommand(ExecuteDeleteCourse, CanExecuteDeleteCourse);
            UpdateCourseCommand = new RelayCommand(ExecuteUpdateCourse, CanExecuteUpdateCourse);

            using (var context = new Dbcontext())
            {
                Courses = new ObservableCollection<Course>(context.Courses);
            }
        }
        #endregion

        #region methods

        // voegt een course toe aan de database
        private void ExecuteAddCourse(object? obj)
        {
            if (Course != null)
            {
                try
                {
                    using (var context = new Dbcontext())
                    {
                        context.Courses.Add(Course);
                        context.SaveChanges();
                        Courses = new ObservableCollection<Course>(context.Courses.ToList());
                    }
                    Course = new Course();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding course: {ex.Message}");
                }
            }
        }


        // kikt of de course niet null is en of de course name niet leeg is.

        private bool CanExecuteAddCourse(object? obj)
        {
            return Course != null && !string.IsNullOrEmpty(Course.CourseName);
        }


        // kijkt of sleected course niet null is en verwijdert een course uit de database 
        private void ExecuteDeleteCourse(object? obj)
        {
            if (SelectedCourse == null) return;
            try
            {
                using (var context = new Dbcontext())
                {
                   
                    var courseToDelete = context.Courses.FirstOrDefault(c => c.Id == SelectedCourse.Id);
                    if (courseToDelete != null)
                    {
                        context.Courses.Remove(courseToDelete);
                        context.SaveChanges();
                        Courses = new ObservableCollection<Course>(context.Courses);
                    }
                }
                SelectedCourse = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting course");
            }
        }

        // kijkt of SelectedCourse geen null is 
        private bool CanExecuteDeleteCourse(object? obj)
        {
            return SelectedCourse != null;
        }

        // kijkt of SelectedCourse niet null is en update de course in de database
        private void ExecuteUpdateCourse(object? obj)
        {
            if (SelectedCourse == null) return;
            try
            {
                using (var context = new Dbcontext())
                {
                   
                    var courseToUpdate = context.Courses.FirstOrDefault(c => c.Id == SelectedCourse.Id);
                    if (courseToUpdate != null)
                    {
                        courseToUpdate.CourseName = Course.CourseName;
                        context.SaveChanges();
                        Courses = new ObservableCollection<Course>(context.Courses);
                    }
                }

                SelectedCourse = null;
                Course = new Course();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating course");
            }
        }


        // kijkt of SelectedCourse niet null is
        private bool CanExecuteUpdateCourse(object? obj)
        {
            return SelectedCourse != null;
        }

        // kijkt of SelectedCourse niet null is en zet de course in de Course property
        private void SelectedCourseChanged()
        {
            if (SelectedCourse != null)
            {
                Course = new Course
                {
                    Id = SelectedCourse.Id,
                    CourseName = SelectedCourse.CourseName
                };
            }
            else
            {
                Course = new Course();
            }
        }
        #endregion
    }
}

