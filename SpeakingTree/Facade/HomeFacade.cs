using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpeakingTree.Data.DataContracts;
using SpeakingTree.Data;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;


namespace SpeakingTree.Facade
{
    public class HomeFacade
    {
        DataFacade dataFacade = new DataFacade();

        #region Masters
        public IEnumerable<Role> GetRoles()
        {
            return dataFacade.GetRoles();
        }

        public IEnumerable<State> GetStates()
        {
            return dataFacade.GetStates();
        }

        public IEnumerable<District> GetDistricts(string stateId)
        {
            return dataFacade.GetDistricts(stateId);
        }

        #endregion

        #region Register & Login
        public int SaveUser(User user, out string userid, out string password)
        {
            userid = string.Empty;
            password = string.Empty;
            return dataFacade.SaveUser(user, out userid, out password);
        }

        public int ValidateUser(string username, string password)
        {
            try
            {
                return dataFacade.ValidateUser(username, password);
            }
            catch (Exception ex)
            {
            }
            return 0;
        }
        #endregion

        #region Enquiry
        public int SaveEnquiry(Enquiry enquiry)
        {
            return dataFacade.SaveEnquiry(enquiry);
        }
        #endregion

        #region User
        public string GetUserPassword(string id, string contactnumber)
        {
            try
            {
                return dataFacade.GetUserPassword(id, contactnumber);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public string GetUserName(string id)
        {
            try
            {
                return dataFacade.GetUserName(id);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public int UpdateUserPassword(string id, string password)
        {
            try
            {
                return dataFacade.UpdateUserPassword(id, password);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion
    }
}