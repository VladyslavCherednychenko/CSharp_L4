using System;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;

using StudentManager.Core;
using StudentManager.MVVM.Model.DataProviders;
using StudentManager.MVVM.Model.DataControllers;

namespace StudentManager.MVVM.ViewModel
{
    internal class StudentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public ObservableCollection<StudentModel> AllStudents { get; set; }
        public ObservableCollection<StudentModel> Students { get; set; }

        private string _currentGroup;
        public string CurrentGroup
        {
            get => _currentGroup;
            set
            {
                _currentGroup = value;
                Students = new ObservableCollection<StudentModel>(AllStudents.Where<StudentModel>(o => o.StudentGroupName == CurrentGroup));
            }
        }

        public RelayCommand DeleteStudentCommand { get; set; }
        public RelayCommand EditStudentCommand { get; set; }
        public RelayCommand AddStudentCommand { get; set; }

        public string _studentPassId;
        public string _studentFirstName;
        public string _studentLastName;
        public string _studentGroupName;

        public string StudentPassId
        {
            get => _studentPassId;
            set
            {
                _studentPassId = value;
                OnPropertyChanged("StudentPassId");
            }
        }
        public string StudentFirstName
        {
            get => _studentFirstName;
            set
            {
                _studentFirstName = value;
                OnPropertyChanged("StudentFirstName");
            }
        }
        public string StudentLastName
        {
            get => _studentLastName;
            set
            {
                _studentLastName = value;
                OnPropertyChanged("StudentLastName");
            }
        }
        public string StudentGroupName
        {
            get => _studentGroupName;
            set
            {
                _studentGroupName = value;
                OnPropertyChanged("StudentGroupName");
            }
        }

        private bool IsEditing = false;
        private StudentModel oldStudent;

        public StudentViewModel()
        {
            DataStudent dataStudent = new DataStudent();
            AllStudents = new ObservableCollection<StudentModel>(dataStudent.CreateStudents());

            DeleteStudentCommand = new RelayCommand(o =>
            {
                Int32.TryParse(o.ToString(), out var _studentPassId);

                var group = AllStudents.FirstOrDefault(x => x.StudentPassId == _studentPassId);

                if (group != null)
                {
                    AllStudents.Remove(group);
                    Students = new ObservableCollection<StudentModel>(AllStudents.Where<StudentModel>(o => o.StudentGroupName == CurrentGroup));
                    OnPropertyChanged(nameof(Students));
                }
                else
                {
                    throw new ArgumentException($"Group named {o} was not found");
                }
            });

            AddStudentCommand = new RelayCommand(o =>
            {
                if (IsEditing)
                {
                    IsEditing = false;

                    Int32.TryParse(StudentPassId, out var id);
                    var newGroup = dataStudent.AddStudent(id, StudentFirstName, StudentLastName, StudentGroupName);
                    var isStudentExsist = (AllStudents.FirstOrDefault(x => x.StudentPassId == newGroup.StudentPassId) != null);

                    if (isStudentExsist)
                    {
                        MessageBox.Show($"Student with Id {newGroup.StudentPassId} is already exsist!");
                    }
                    else
                    {
                        var count = AllStudents.Where<StudentModel>(o => o.StudentGroupName == StudentGroupName).Count();
                        if (count < 35)
                        {
                            var student = AllStudents.FirstOrDefault(x => x.StudentPassId == oldStudent.StudentPassId);

                            var index = AllStudents.IndexOf(student);
                            AllStudents[index] = newGroup;

                            Students = new ObservableCollection<StudentModel>(AllStudents.Where<StudentModel>(o => o.StudentGroupName == CurrentGroup));
                            OnPropertyChanged(nameof(Students));
                        }
                        else
                        {
                            MessageBox.Show($"Group {StudentGroupName} is already have 35 students!");
                        }
                    }
                }
                else
                {
                    Int32.TryParse(StudentPassId, out var id);
                    var student = AllStudents.FirstOrDefault(x => x.StudentPassId == id);
                    if (student == null)
                    {
                        var count = AllStudents.Where<StudentModel>(o => o.StudentGroupName == StudentGroupName).Count();
                        if (count < 35)
                        {
                            try
                            {
                                var newStudent = dataStudent.AddStudent(id, StudentFirstName, StudentLastName, StudentGroupName);
                                AllStudents.Add(newStudent);
                                Students = new ObservableCollection<StudentModel>(AllStudents.Where<StudentModel>(o => o.StudentGroupName == CurrentGroup));
                                OnPropertyChanged(nameof(Students));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Group {StudentGroupName} is already have 35 students!");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Student with id {id} is already exsist!");
                    }
                }
            });

            EditStudentCommand = new RelayCommand(o =>
            {
                Int32.TryParse(o.ToString(), out var id);
                oldStudent = AllStudents.FirstOrDefault(x => x.StudentPassId == id);

                if (oldStudent != null)
                {
                    StudentPassId = oldStudent.StudentPassId.ToString();
                    StudentFirstName = oldStudent.StudentFirstName;
                    StudentLastName = oldStudent.StudentLastName;
                    StudentGroupName = oldStudent.StudentGroupName;

                    IsEditing = true;
                }
                else
                {
                    throw new ArgumentException($"Student with id {o} was not found");
                }
            });
        }
    }
}
