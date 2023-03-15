using System.Runtime.Serialization;

namespace StudentManager.MVVM.Model.DataProviders
{
    [DataContract]
    public class StudentModel
    {
        [DataMember]
        public int StudentPassId { get; set; }

        [DataMember]
        public string StudentFirstName { get; set; }

        [DataMember]
        public string StudentLastName { get; set; }

        [DataMember]
        public string StudentGroupName { get; set; }

        public string StudentFullName
        {
            get
            {
                return $"{StudentFirstName} {StudentLastName}";
            }
        }
    }
}
