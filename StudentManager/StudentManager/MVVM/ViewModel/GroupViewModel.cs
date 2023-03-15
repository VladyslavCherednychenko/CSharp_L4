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
    internal class GroupViewModel : INotifyPropertyChanged
    {
        private GroupModel oldGroup;

        private string _groupFaculty;
        private string _groupSpeciality;
        private string _groupYear;
        private string _groupNumber;

        public string GroupFaculty
        {
            get => _groupFaculty;
            set
            {
                _groupFaculty = value;
                OnPropertyChanged("GroupFaculty");
            }
        }
        public string GroupSpeciality
        {
            get => _groupSpeciality;
            set
            {
                _groupSpeciality = value;
                OnPropertyChanged("GroupSpeciality");
            }
        }
        public string GroupYear
        {
            get => _groupYear;
            set
            {
                _groupYear = value;
                OnPropertyChanged("GroupYear");
            }
        }
        public string GroupNumber
        {
            get => _groupNumber;
            set
            {
                _groupNumber = value;
                OnPropertyChanged("GroupNumber");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public ObservableCollection<GroupModel> Groups { get; set; }
        public RelayCommand EditGroupCommand { get; set; }
        public RelayCommand DeleteGroupCommand { get; set; }
        public RelayCommand AddGroupCommand { get; set; }

        private bool IsEditing = false;

        public GroupViewModel()
        {
            DataGroup dataGroup = new DataGroup();
            Groups = new ObservableCollection<GroupModel>(dataGroup.CreateGroups());

            DeleteGroupCommand = new RelayCommand(o =>
            {
                var group = Groups.FirstOrDefault(x => x.GroupName == o.ToString());

                if (group != null)
                {
                    Groups.Remove(group);
                }
                else
                {
                    throw new ArgumentException($"Group named {o} was not found");
                }
            });

            AddGroupCommand = new RelayCommand(o =>
            {
                if (IsEditing)
                {
                    IsEditing = false;

                    Int32.TryParse(GroupYear, out var year);
                    Int32.TryParse(GroupNumber, out var num);
                    var newGroup = dataGroup.AddGroup(GroupFaculty, GroupSpeciality, year, num);
                    var isGroupExsist = (Groups.FirstOrDefault(x => x.GroupName == newGroup.GroupName) != null);

                    if (isGroupExsist)
                    {
                        MessageBox.Show($"Group with name {newGroup.GroupName} is already exsist!");
                    }
                    else
                    {
                        var group = Groups.FirstOrDefault(x => x.GroupName == oldGroup.GroupName);

                        var index = Groups.IndexOf(group);
                        Groups[index] = newGroup;
                    }
                }
                else
                {
                    Int32.TryParse(GroupYear, out var year);
                    Int32.TryParse(GroupNumber, out var num);
                    var fullGroupName = $"{GroupFaculty}{GroupSpeciality}-{year}-{num}";
                    var group = Groups.FirstOrDefault(x => x.GroupName == fullGroupName);
                    if (group == null)
                    {
                        try
                        {
                            var newGroup = dataGroup.AddGroup(GroupFaculty, GroupSpeciality, year, num);
                            Groups.Add(newGroup);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Group with name {fullGroupName} is already exsist!");
                    }
                }
            });

            EditGroupCommand = new RelayCommand(o =>
            {
                oldGroup = Groups.FirstOrDefault(x => x.GroupName == o.ToString());

                if (oldGroup != null)
                {
                    GroupFaculty = oldGroup.GroupFaculty;
                    GroupSpeciality = oldGroup.GroupSpeciality;
                    GroupYear = oldGroup.GroupYear.ToString();
                    GroupNumber = oldGroup.GroupNumber.ToString();

                    IsEditing = true;
                }
                else
                {
                    throw new ArgumentException($"Group named {o} was not found");
                }
            });
        }
    }
}
