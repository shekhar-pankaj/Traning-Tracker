
using System.Web.UI;
using TrainingTracker.DAL.DataAccess;
using TrainingTracker.DAL.Interface;

namespace TrainingTracker.BL.Base
{
    public class BussinessBase
    {
        public ISessionDal SessionDataAccesor
        {
            get
            {
                return new SessionDal();
            }
        }

        public ISkillDal SkillDataAccesor
        {
            get
            {
                return new SkillDal();
            }
        }

        public IUserDal UserDataAccesor
        {
            get
            {
                return new UserDal();
            }
        }

        public IProjectDal ProjectDataAccesor
        {
            get
            {
                return new ProjectDal();
            }
        }

        public IFeedbackDal FeedbackDataAccesor
        {
            get
            {
                return new FeedbackDal();
            }
        }

    }
}