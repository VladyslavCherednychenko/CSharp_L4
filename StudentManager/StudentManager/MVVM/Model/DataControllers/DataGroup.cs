using StudentManager.MVVM.Model.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.MVVM.Model.DataControllers
{
    internal class DataGroup
    {
        readonly string groupFaculty = "CEC";
        readonly string groupSpeciality = "CE";
        readonly int groupYear = 23;

        public List<GroupModel> CreateGroups(int total = 2)
        {
            List<GroupModel> output = new List<GroupModel>();

            for (int i = 0; i < total; i++)
            {

                output.Add(CreateGroup(i + 1));
            }

            return output;
        }

        public GroupModel AddGroup(string gFaculty, string gSpeciality, int gYear, int gNumber)
        {
            GroupModel output = new GroupModel();

            output.GroupNumber = gNumber;
            output.GroupYear = gYear;
            output.GroupSpeciality = gSpeciality;
            output.GroupFaculty = gFaculty;

            return output;
        }

        private GroupModel CreateGroup(int id)
        {
            GroupModel output = new GroupModel();

            output.GroupNumber = id;
            output.GroupYear = groupYear;
            output.GroupSpeciality = groupSpeciality;
            output.GroupFaculty = groupFaculty;

            return output;
        }
    }
}
