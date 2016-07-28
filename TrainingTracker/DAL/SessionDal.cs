using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TrainingTracker.Models;
using TrainingTracker.Utility;

namespace TrainingTracker.DAL
{
    public class SessionDal
    {
        public List<Session> GetSessionsByUserId(int userId)
        {
            var sessions = new List<Session>();

            var prms = new List<SqlParameter>
            {
                SqlUtility.CreateParameter(SPGetSessionsByUserID.PARAM_USER_ID, SqlDbType.Int, userId)
            };

            try
            {
                var dt = SqlUtility.ExecuteAndGetTable(SPGetSessionsByUserID.NAME,
                    CommandType.StoredProcedure, SPGetSessionsByUserID.TABLE_NAME, prms);

                sessions.AddRange(from DataRow row in dt.Rows
                                  select new Session
                                  {
                                      Title = row["Title"].ToString(),
                                      SessionId = Convert.ToInt32(row["SessionId"])
                                  });
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }

            return sessions;
        }
    }
}