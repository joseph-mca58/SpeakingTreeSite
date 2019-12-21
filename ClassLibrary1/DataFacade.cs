using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeakingTree.Data.AccessProviders;
using SpeakingTree.Data;
using SpeakingTree.Data.DataContracts;

namespace SpeakingTree.Data
{
    public class DataFacade
    {
        private readonly ISQLAccessProvider _SqlAccessProvider;

        internal ISQLAccessProvider ISqlAccessProvider
        {
            get { return _SqlAccessProvider; }
        }


        public DataFacade()
        {
            _SqlAccessProvider = new SQLAccessProvider();
        }

        public void SQLAccessProvider() { }

        #region Masters
        public IEnumerable<Role> GetRoles()
        {
            return ISqlAccessProvider.GetRoles();
        }
        public IEnumerable<State> GetStates()
        {
            return ISqlAccessProvider.GetStates();
        }
        public IEnumerable<District> GetDistricts(string stateId)
        {
            return ISqlAccessProvider.GetDistricts(stateId);
        }

        #endregion

        #region Enquiry
        public int SaveEnquiry(Enquiry enquiry)
        {
            return ISqlAccessProvider.SaveEnquiry(enquiry);
        }
        #endregion

        #region Register & Login
        public int SaveUser(User user, out string userid, out string password)
        {
            userid = string.Empty;
            password = string.Empty;
            return ISqlAccessProvider.SaveUser(user, out userid, out password);
        }

        public int ValidateUser(string username, string password)
        {
            try
            {
                return ISqlAccessProvider.ValidateUser(username, password);
            }
            catch (Exception ex)
            {
            }
            return 0;
        }

        #endregion

        #region User
        public string GetUserName(string id)
        {
            try
            {
                return ISqlAccessProvider.GetUserName(id);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public string GetUserPassword(string id,string contactnumber)
        {
            try
            {
                return ISqlAccessProvider.GetUserPassword(id, contactnumber);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public int UpdateUserPassword(string id,string password)
        {
            try
            {
                return ISqlAccessProvider.UpdateUserPassword(id, password);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion

    }
}
