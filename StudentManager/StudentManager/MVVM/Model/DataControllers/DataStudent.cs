using StudentManager.MVVM.Model.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.MVVM.Model.DataControllers
{
    internal class DataStudent
    {
        public List<StudentModel> CreateStudents(int total = 34)
        {
            List<StudentModel> output = new List<StudentModel>();

            for (int i = 0; i < total; i++)
            {

                output.Add(CreateStudent(i + 1));
            }

            return output;
        }

        public StudentModel AddStudent(int sPassId, string sFirstName, string sLastName, string sGroupName)
        {
            StudentModel output = new StudentModel();

            output.StudentPassId = sPassId;
            output.StudentFirstName = sFirstName;
            output.StudentLastName = sLastName;
            output.StudentGroupName = sGroupName;

            return output;
        }

        private StudentModel CreateStudent(int id)
        {
            StudentModel output = new StudentModel();
            Random rnd = new Random();

            output.StudentPassId = rnd.Next(1000000);
            output.StudentFirstName = "FirstName_" + id;
            output.StudentLastName = "LastName_" + id;
            output.StudentGroupName = "CECCE-23-1";

            return output;
        }
    }
}
