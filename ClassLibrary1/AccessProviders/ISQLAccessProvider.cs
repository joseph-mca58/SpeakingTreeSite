using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeakingTree.Data.DataContracts;

namespace SpeakingTree.Data.AccessProviders
{
    interface ISQLAccessProvider
    {

        #region Masters
        IEnumerable<Role> GetRoles();

        IEnumerable<State> GetStates();

        IEnumerable<District> GetDistricts(string stateId);

        int SaveEnquiry(Enquiry enquiry);

        #endregion


        #region Register & Login
        int SaveUser(User user,out string userid, out string password);

        int ValidateUser(string username, string password);
        #endregion

        #region User
        string GetUserName(string id);

        string GetUserPassword(string id, string contactnumber);

        
        int UpdateUserPassword(string id, string password);

        #endregion
    }
}
