using System;
using System.Runtime.Serialization;

namespace StudentManager.MVVM.Model.DataProviders
{
    [DataContract]
    public class GroupModel
    {
        private int groupYear;
        private int groupNumber;

        [DataMember]
        public string GroupFaculty { get; set; }

        [DataMember]
        public string GroupSpeciality { get; set; }

        [DataMember]
        public int GroupYear
        {
            get => groupYear;
            set
            {
                if (value > 0 && value < 70)
                {
                    groupYear = value;
                }
                else
                {
                    throw new ArgumentException($"Year of the group should be in range between 0 and 70");
                }
            }
        }

        [DataMember]
        public int GroupNumber
        {
            get => groupNumber;
            set
            {
                if (value > 0 && value < 10)
                {
                    groupNumber = value;
                }
                else
                {
                    throw new ArgumentException($"Number of the group should be in range between 0 and 10");
                }
            }
        }

        public string GroupName
        {
            get
            {
                return $"{GroupFaculty}{GroupSpeciality}-{GroupYear}-{GroupNumber}";
            }
        }

    }
}
