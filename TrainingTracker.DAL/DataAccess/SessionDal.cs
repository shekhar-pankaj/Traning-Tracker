using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TrainingTracker.Common.Entity;
using TrainingTracker.Common.Utility;
using TrainingTracker.DAL.Interface;


namespace TrainingTracker.DAL.DataAccess
{
    public class SessionDal : ISessionDal
    {
        public List<Session> GetSessionsByUserId( int userId )
        {
            var sessions = new List<Session>();

            var prms = new List<SqlParameter>
            {
                SqlUtility.CreateParameter(SPGetSessionsByUserID.PARAM_USER_ID, SqlDbType.Int, userId)
            };

            try
            {
                var dt = SqlUtility.ExecuteAndGetTable(SPGetSessionsByUserID.NAME ,
                    CommandType.StoredProcedure , SPGetSessionsByUserID.TABLE_NAME , prms);

                sessions.AddRange(from DataRow row in dt.Rows
                                  select new Session
                                  {
                                      Title = row["Title"].ToString() ,
                                     // SessionId = Convert.ToInt32(row["SessionId"])
                                  });
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }

            return sessions;
        }

        /// <summary>
        /// Inteface method to fetch sessions by User id
        /// </summary>
        /// <param name="objSession">User Id</param>
        /// <returns>List Of session</returns>
        public bool AddEditSessions( Session objSession )
        {
            var prms = new List<SqlParameter>
            {
                SqlUtility.CreateParameter(SPAddEditSession.PARAM_PRESENTER, SqlDbType.NVarChar, objSession.Presenter),
                SqlUtility.CreateParameter(SPAddEditSession.PARAM_ID, SqlDbType.Int, objSession.Id),
                SqlUtility.CreateParameter(SPAddEditSession.PARAM_TITLE, SqlDbType.NVarChar, objSession.Title),
                SqlUtility.CreateParameter(SPAddEditSession.PARAM_DESCRIPTION, SqlDbType.NVarChar, objSession.Description),
                SqlUtility.CreateParameter(SPAddEditSession.PARAM_DATE, SqlDbType.DateTime, objSession.Date),
                SqlUtility.CreateParameter(SPAddEditSession.PARAM_ATTENDEES, SqlDbType.NVarChar,  string.Join(",",objSession.Attendee))

            };

            try
            {
                var rowsAffected = SqlUtility.ExecuteNonQuery(SPAddEditSession.NAME ,
                    CommandType.StoredProcedure ,
                    prms);
                return (rowsAffected > 0);
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="sessionType"></param>
        /// <param name="searchKeyword"></param>
        /// <returns></returns>
        public List<Session> GetSessionOnFilter( int pageSize , int sessionType , string searchKeyword )
        {
            List<Session> sessions =new List<Session>();
            var prms = new List<SqlParameter>
            {
                SqlUtility.CreateParameter(SPGetSessionsOnFilter.PARAM_KEYWORD, SqlDbType.NVarChar, searchKeyword),
                SqlUtility.CreateParameter(SPGetSessionsOnFilter.PARAM_PAGESIZE, SqlDbType.Int, pageSize),
                SqlUtility.CreateParameter(SPGetSessionsOnFilter.PARAM_SESSIONTYPE, SqlDbType.Int, sessionType),
              
            };
            try
            {
                var dt = SqlUtility.ExecuteAndGetTable(SPGetSessionsOnFilter.NAME ,
                    CommandType.StoredProcedure , SPGetSessionsOnFilter.TABLE_NAME , prms);


                foreach (DataRow row in dt.Rows)
                {
                 //   var sessionID = Convert.ToInt32(row["SessionId"]);
                    if (sessions.All(x => x.Id != Convert.ToInt32(row["SessionId"])))
                    {
                        sessions.Add(new Session
                        {
                            Title = row["Title"].ToString() ,
                                      Id = Convert.ToInt32(row["SessionId"]),
                                      Date = Convert.ToDateTime(row["SessionDate"]),
                                      Description = row["Description"].ToString(),
                                      PresenterFullName =row["PresenterFullName"].ToString(),
                                      Presenter = Convert.ToInt32(row["Presenter"]),
                                      SessionAttendees = new List<User>()
                        });
                    }

                    int userId;
                    var objSession = sessions.FirstOrDefault(x => x.Id == Convert.ToInt32(row["SessionId"]));

                    if (objSession == null || !Int32.TryParse(row["UserId"].ToString(), out userId) ) continue;

                    User objUser = new User
                    {
                        UserId = Convert.ToInt32(row["UserId"]) ,
                        FirstName = row["FirstName"].ToString() ,
                        LastName = row["LastName"].ToString()
                    };

                    objSession.SessionAttendees.Add(objUser);
                }

                //sessions.AddRange(from DataRow row in 
                //                  select new Session
                //                  {
                //                      Title = row["Title"].ToString() ,
                //                      Id = Convert.ToInt32(row["SessionId"]),
                //                      Date = Convert.ToDateTime(row["SessionDate"]),
                //                      Description = row["Description"].ToString(),
                //                      PresenterFullName =row["PresenterFullName"].ToString(),
                //                      Presenter = Convert.ToInt32(row["Presenter"])
                //                  });
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }
            return sessions;
        }
    }
}