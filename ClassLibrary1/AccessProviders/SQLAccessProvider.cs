using System;
using System.Collections.Generic;
using System.Configuration;
using SpeakingTree.Data.DataContracts;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace SpeakingTree.Data.AccessProviders
{
    class SQLAccessProvider : ISQLAccessProvider
    {
        private string SqlConnectionString
        {
            get { return Convert.ToString(ConfigurationManager.ConnectionStrings["STConStr"].ConnectionString); }
        }

        #region Masters
        public IEnumerable<Role> GetRoles()
        {
            try
            {
                using (var sqlConnection = new SqlConnection(SqlConnectionString))
                {
                    sqlConnection.Open();
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "pr_GetAllRoles";
                        var adapter = new SqlDataAdapter(command);
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        return dt.Rows.Count > 0
                            ? (dt.AsEnumerable().Select(p => new Role
                            {
                                Id = p.Field<int>("Id"),
                                Name = p.Field<string>("Name").Trim()
                            }))
                            : null;
                    }
                }
            }
            catch (Exception ex)
            {
                // add log to tblError
            }
            return null;
        }

        public IEnumerable<State> GetStates()
        {
            try
            {
                using (var sqlConnection = new SqlConnection(SqlConnectionString))
                {
                    sqlConnection.Open();
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "pr_GetAllStates";
                        var adapter = new SqlDataAdapter(command);
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        return dt.Rows.Count > 0
                            ? (dt.AsEnumerable().Select(p => new State
                            {
                                Id = p.Field<int>("Id"),
                                Name = p.Field<string>("Name").Trim()
                            }))
                            : null;
                    }
                }
            }
            catch (Exception ex)
            {
                // add log to tblError
            }
            return null;
        }

        public IEnumerable<District> GetDistricts(string stateId)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(SqlConnectionString))
                {
                    sqlConnection.Open();
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "pr_GetDistricts";
                        command.Parameters.AddWithValue("@StateId", stateId);
                        var adapter = new SqlDataAdapter(command);
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        return dt.Rows.Count > 0
                            ? (dt.AsEnumerable().Select(p => new District
                            {
                                Id = p.Field<int>("Id"),
                                StateId = p.Field<int>("StateId"),
                                Name = p.Field<string>("Name").Trim()
                            }))
                            : null;
                    }
                }
            }
            catch (Exception ex)
            {
                // add log to tblError
            }
            return null;
        }


        public int SaveEnquiry(Enquiry enquiry)
        {
            int result = 0;
            try
            {
                using (var sqlConnection = new SqlConnection(SqlConnectionString))
                {
                    sqlConnection.Open();
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "pr_addEnquiry";

                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = enquiry.Name;
                        command.Parameters.Add("@School", SqlDbType.VarChar).Value = enquiry.School;
                        command.Parameters.Add("@EmailId", SqlDbType.VarChar).Value = enquiry.EmailId;
                        command.Parameters.Add("@contactnumber", SqlDbType.VarChar).Value = enquiry.ContactNo;
                        command.Parameters.Add("@query", SqlDbType.VarChar).Value = enquiry.Query;

                        result = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion

        #region Register & Login
        public int SaveUser(User user, out string userid, out string password)
        {
            int rowsAffected = 0;
            userid = string.Empty;
            password = string.Empty;
            try
            {
                using (var sqlConnection = new SqlConnection(SqlConnectionString))
                {
                    sqlConnection.Open();
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "pr_AddUpdateUser";

                        command.Parameters.Add("@Id", SqlDbType.VarChar).Value = Convert.ToString(user.Id);
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = user.Name;
                        command.Parameters.Add("@RoleId", SqlDbType.Int).Value = user.RoleId;
                        if (string.IsNullOrEmpty(user.Id))
                        {
                            char ch;
                            Random random = new Random();
                            for (int i = 0; i < 6; i++)
                            {
                                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                                user.Password += ch;
                            }
                        }
                        command.Parameters.Add("@Password", SqlDbType.VarChar).Value = user.Password;
                        command.Parameters.Add("@City", SqlDbType.VarChar).Value = user.City;
                        command.Parameters.Add("@State", SqlDbType.Int).Value = user.State;
                        command.Parameters.Add("@District", SqlDbType.Int).Value = user.District;
                        command.Parameters.Add("@Area", SqlDbType.VarChar).Value = user.Area;
                        command.Parameters.Add("@Pincode", SqlDbType.Int).Value = user.Pincode;
                        command.Parameters.Add("@Address", SqlDbType.VarChar).Value = user.Address;
                        command.Parameters.Add("@EmailId", SqlDbType.VarChar).Value = user.EmailId;
                        command.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = user.ContactNo;

                        //SCHOOL

                        command.Parameters.Add("@SchoolId", SqlDbType.VarChar).Value = user.school.Id;
                        command.Parameters.Add("@Branch", SqlDbType.VarChar).Value = user.school.Branch;
                        command.Parameters.Add("@ExamOption", SqlDbType.VarChar).Value = user.school.ExamOption;
                        command.Parameters.Add("@Strength", SqlDbType.VarChar).Value = user.school.Strength;
                        command.Parameters.Add("@PersonContact", SqlDbType.VarChar).Value = user.school.PersonContact;

                        //STUDENT

                        command.Parameters.Add("@StudetId", SqlDbType.VarChar).Value = user.student.Id;
                        command.Parameters.Add("@class", SqlDbType.VarChar).Value = user.student.StudentClass;
                        command.Parameters.Add("@ParentName", SqlDbType.VarChar).Value = user.student.ParentName;
                        command.Parameters.Add("@UserID", SqlDbType.VarChar, 15).Direction = ParameterDirection.Output;
                        rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            userid = (string)command.Parameters["@UserID"].Value;
                            password = user.Password;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return rowsAffected;
        }

        public int ValidateUser(string username, string password)
        {
            int role = 0;
            try
            {
                using (var sqlConnection = new SqlConnection(SqlConnectionString))
                {
                    sqlConnection.Open();
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "pr_ValidateUser";

                        command.Parameters.Add("@Username", SqlDbType.VarChar).Value = username;
                        command.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                        role = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return role;


        }
        #endregion

        #region User
        public string GetUserName(string id)
        {
            string username = string.Empty;
            try
            {
                using (var sqlConnection = new SqlConnection(SqlConnectionString))
                {
                    sqlConnection.Open();
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "pr_GetUserName";

                        command.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
                        username = Convert.ToString(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            return username;

        }

        public string GetUserPassword(string id, string contactnumber)
        {
            string password = string.Empty;
            try
            {
                using (var sqlConnection = new SqlConnection(SqlConnectionString))
                {
                    sqlConnection.Open();
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "pr_GetUserPassword";

                        command.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
                        command.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = contactnumber;
                        password = Convert.ToString(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            return password;

        }

        public int UpdateUserPassword(string id, string password)
        {
            int result = 0;
            try
            {
                using (var sqlConnection = new SqlConnection(SqlConnectionString))
                {
                    sqlConnection.Open();
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "pr_UpdateUserPassword";

                        command.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
                        command.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                        result = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        #endregion



    }
}
