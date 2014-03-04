using BCUtility;
using HealthyClub.Administration.EDS;
using HealthyClub.EDM;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthyClub.Administration.DA
{
    public class MembershipHelper
    {
        public string GetConfirmationCode(string username)
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.Users
                        where u.UserName == username
                        select u;

            return GetConfirmationCode(query.FirstOrDefault().ID);

        }

        public string GetConfirmationCode(Guid UserID)
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.Users
                        where u.UserId == UserID
                        select u;

            return GetConfirmationCode(query.FirstOrDefault().ID);

        }

        public string GetConfirmationCode(int userID)
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.webpages_Membership
                        where u.UserId == userID
                        select u;
            if (query.FirstOrDefault() != null)
            {
                return query.FirstOrDefault().ConfirmationToken;
            }
            else return null;

        }

        public int GetUserID(Guid userID)
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.Users
                        where u.UserId == userID
                        select u;
            if (query.FirstOrDefault() != null)
                return query.FirstOrDefault().ID;
            else return 0;
        }

        public Guid GetProviderUserKey(int userID)
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.Users
                        where u.ID == userID
                        select u;

            return query.FirstOrDefault().UserId;
        }

        public Guid GetProviderUserKey(string username)
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.Users
                        where u.UserName == username
                        select u;

            return query.FirstOrDefault().UserId;
        }

        public static void ResetPasswordToken(int userID)
        {
            HCEntities ent = new HCEntities();

            var query = from m in ent.webpages_Membership
                        where m.UserId == userID
                        select m;

            webpages_Membership member = query.FirstOrDefault();
            if (member != null)
            {
                member.PasswordVerificationToken = null;
                member.PasswordVerificationTokenExpirationDate = null;
                ent.SaveChanges();
            }
        }

        public AdministrationEDSC.UsersDTDataTable GetAllUsers()
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.Users
                        select u;

            var users = query.AsEnumerable();
            if (users != null)
            {
                var dt = new AdministrationEDSC.UsersDTDataTable();
                BCUtility.ObjectHandler.CopyEnumerableToDataTable(users, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public AdministrationEDSC.v_UserExplorerDTDataTable GetAllUsersinRole(String role)
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.v_UserExplorer
                        where u.RoleName == role
                        select u;

            var users = query.AsEnumerable();
            if (users != null)
            {
                var dt = new AdministrationEDSC.v_UserExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(users, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public void DeleteUser(string username)
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.Users
                        where u.UserName == username
                        select u;

            Users user = query.FirstOrDefault();
            if (user != null)
            {
                ent.DeleteObject(user);
                ent.SaveChanges();
            }
        }
    }


}
