using EindopdrachtDesktop1.ViewModel;
using MenuNavigatie.Helpers;
using System.Windows.Input;

public class MainViewModel : ObservableObject
{
    #region fields
    private object _activeViewModel;

    #endregion

    #region constructors
    public MainViewModel()
    {
        _activeViewModel = new StudentsViewModel();
        ShowStudentsCommand = new RelayCommand(ExecuteShowStudents);
        ShowCoursesCommand = new RelayCommand(ExecuteShowCourses);
    }
    #endregion

    #region properties
    public object ActiveViewModel
    {
        get { return _activeViewModel; }
        set
        {
            if (_activeViewModel != value)
            {
                _activeViewModel = value;
                OnPropertyChanged();
            }
        }
    }
    #endregion

    #region commands
  
    public ICommand ShowStudentsCommand { get; set; }
    public ICommand ShowCoursesCommand { get; set; }

    #endregion

    #region methods

    private void ExecuteShowStudents(object? obj)
    {
        ActiveViewModel = new StudentsViewModel(); 
    }

    private void ExecuteShowCourses(object? obj)
    {
        ActiveViewModel = new CoursesViewModel();
    }
    #endregion
}
