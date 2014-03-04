using HealthyClub.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyClub.Customer.DA
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
    }
}
