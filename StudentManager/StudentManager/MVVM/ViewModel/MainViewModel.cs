using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows;
using StudentManager.Core;
using StudentManager.MVVM.Model.DataProviders;

namespace StudentManager.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand GroupViewCommand { get; set; }
        public RelayCommand StudentViewCommand { get; set; }
        public RelayCommand ImportJsonCommand { get; set; }
        public RelayCommand ExportJsonCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public GroupViewModel GroupsVM { get; set; }
        public StudentViewModel StudentVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { 
                _currentView = value; 
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            GroupsVM = new GroupViewModel();
            StudentVM = new StudentViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            GroupViewCommand = new RelayCommand(o =>
            {
                CurrentView = GroupsVM;
            });

            StudentViewCommand = new RelayCommand(o =>
            {
                CurrentView = StudentVM;
                StudentVM.CurrentGroup = o.ToString();
            });

            ImportJsonCommand = new RelayCommand(o =>
            {
                MyJsonObject jsonObject;
                var serializer = new DataContractJsonSerializer(typeof(MyJsonObject));

                using (var file = new FileStream("file.json", FileMode.OpenOrCreate))
                {
                    try
                    {
                        jsonObject = (MyJsonObject)serializer.ReadObject(file);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        jsonObject = null;
                    }
                }

                if (jsonObject != null)
                {
                    GroupsVM.Groups = new ObservableCollection<GroupModel>(jsonObject.Groups);
                    StudentVM.AllStudents = new ObservableCollection<StudentModel>(jsonObject.Students);
                }
            });

            ExportJsonCommand = new RelayCommand(o =>
            {
                var jsonObject = new MyJsonObject
                {
                    Groups = GroupsVM.Groups.ToList(),
                    Students = StudentVM.AllStudents.ToList()
                };

                var serializer = new DataContractJsonSerializer(typeof(MyJsonObject));

                using (var file = new FileStream("file.json", FileMode.Create))
                {
                    serializer.WriteObject(file, jsonObject);
                }
            });
        }

        [DataContract(Name = "root")]
        public class MyJsonObject
        {
            [DataMember(Name = "groups")]
            public List<GroupModel> Groups { get; set; }

            [DataMember(Name = "students")]
            public List<StudentModel> Students { get; set; }
        }
    }
}
