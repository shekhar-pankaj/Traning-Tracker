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
    public class SkillDal:ISkillDal
    {
        public List<Skill> GetSkillsByUserId(int userId)
        {
            var skills = new List<Skill>();

            var prms = new List<SqlParameter>
            {
                SqlUtility.CreateParameter(SPGetSkillsByUserID.PARAM_USER_ID, SqlDbType.Int, userId)
            };

            try
            {
                var dt = SqlUtility.ExecuteAndGetTable(SPGetSkillsByUserID.NAME,
                    CommandType.StoredProcedure, SPGetSkillsByUserID.TABLE_NAME, prms);

                skills.AddRange(from DataRow row in dt.Rows
                                  select new Skill
                                  {
                                      Name = row["Name"].ToString(),
                                      SkillId = Convert.ToInt32(row["SkillId"]),
                                      Rating = (row["Rating"] == DBNull.Value) ? 0 : Convert.ToInt32(row["Rating"])
                                  });
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }

            return skills;
        }

        public List<Skill> GetAllSkillsForApp()
        {
            var skills = new List<Skill>();
          
            try
            {
                var dt = SqlUtility.ExecuteAndGetTable(SPGetApplicationSkills.NAME ,
                    CommandType.StoredProcedure , SPGetApplicationSkills.TABLE_NAME , null);

                skills.AddRange(from DataRow row in dt.Rows
                                select new Skill
                                {
                                    Name = row["Name"].ToString() ,
                                    SkillId = Convert.ToInt32(row["SkillId"]) ,                                  
                                });
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }

            return skills;
        }

    }
}