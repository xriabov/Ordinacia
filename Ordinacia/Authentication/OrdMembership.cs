using System;
using System.Linq;
using System.Web.Security;
using Ordinacia.Data_Access;

namespace Ordinacia.Authentication
{
    public class OrdMembership : MembershipProvider
    {
        public override void UpdateUser(MembershipUser user)
        {
            throw new System.NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            using (AuthenticationDB authContext = new AuthenticationDB())
            {
                var user = (from u in authContext.Usrs
                    where string.Compare(u.UserName, username, StringComparison.OrdinalIgnoreCase) == 0
                          && string.Compare(u.UserPassword, password) == 0
                    select u).FirstOrDefault();
                return user != null;
            }
        }
        
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (AuthenticationDB authContext = new AuthenticationDB())
            {
                var user = (from usr in authContext.Usrs
                    where String.Compare(username, usr.UserName, StringComparison.Ordinal) == 0
                    select usr).FirstOrDefault();

                if (user == null)
                    return null;
                var selectedUser = new OrdMembershipUser(user);
                return selectedUser;
            }
        }

        public override string ApplicationName
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get => throw new System.NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get => throw new System.NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get => throw new System.NotImplementedException();
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get => throw new System.NotImplementedException();
        }

        public override int MinRequiredPasswordLength
        {
            get => throw new System.NotImplementedException();
        }

        public override int PasswordAttemptWindow
        {
            get => throw new System.NotImplementedException();
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get => throw new System.NotImplementedException();
        }

        public override string PasswordStrengthRegularExpression
        {
            get => throw new System.NotImplementedException();
        }

        public override bool RequiresQuestionAndAnswer
        {
            get => throw new System.NotImplementedException();
        }

        public override bool RequiresUniqueEmail
        {
            get => throw new System.NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
            string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new System.NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        
        public override string GetUserNameByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}