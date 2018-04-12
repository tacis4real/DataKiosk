﻿using System.Collections.Generic;
using DataKioskStacks.APIServer.APIObjs;
using DataKioskStacks.DataContract.Admin;
using DataKioskStacks.Repository;

namespace DataKioskStacks.APIs
{
    public class PortalUser
    {
        public static UserRegResponse AddUser(UserRegistrationObj user)
        {
            return new UserRepository().AddUser(user);
        }

        public static bool DeleteUser(long agentUserId)
        {
            return true;
            //return new UserRepository().DeleteUser(agentUserId);
        }

        public static UserDeviceResponseObj AddUserDevice(UserDeviceRegObj userDevice)
        {
            return null;
            //return new UserRepository().AddUserDevice(userDevice);
        }

        public static UserDeviceResponseObj AddNotificationToken(UserNotificationTokenObj userNotificationToken)
        {
            return null;
            //return new UserRepository().AddNotificationToken(userNotificationToken);
        }

        public static UserDeviceResponseObj AuthorizeUserDevice(UserAuthorizeCodeObj authorizeCode)
        {
            return null;
            //return new UserRepository().AuthorizeUserDevice(authorizeCode);
        }

        public static UserRegResponse UpdateUser(UserEditObj editObj)
        {
            return null;
            //return new UserRepository().UpdateUser(editObj);
        }

        public static ResponseStatus UpdateUser(UserRegistrationObj user)
        {
            return new UserRepository().UpdateUser(user);
        }

        public static RegisteredUserReportObj GetUser(long userId)
        {
            return null;
            //return new UserRepository().GetUserObj(userId);
        }

        public static List<User> GetUsers()
        {
            return null;
            //return new UserRepository().GetUsers();
        }

        public static List<RegisteredUserReportObj> GetUserList()
        {
            return new UserRepository().GetUserObjs();
        }

        public static User GetUser(string username)
        {
            return null;
            //return new UserRepository().GetUser(username);
        }

        public static User GetRawUser(long userId)
        {
            return null;
            //return new UserRepository().GetUser(userId);
        }

        public static AdminTaskResponseObj ResetPassword(long adminUserId, string token, string targetUsername)
        {
            return null;
            //return new UserRepository().ResetPassword(adminUserId, token, targetUsername);
        }
        public static AdminTaskResponseObj ResetPassword(string targetUsername)
        {
            return null;
            //return new UserRepository().ResetPassword(targetUsername);
        }

        public static UserResponseObj ChangePassword(string username, string oldPassword, string newPassword, string token)
        {
            return null;
            //return new UserRepository().ChangePassword(username, oldPassword, newPassword, token);
        }

        public static UserResponseObj ChangePassword(string username, string oldPassword, string newPassword)
        {
            return null;
            //return new UserRepository().ChangePassword(username, oldPassword, newPassword);
        }
        public static UserResponseObj ActivateDeActivateUser(long adminUserId, string token, string targetUsername, bool status)
        {
            return null;
            //return new UserRepository().ActivateDeActivateUser(adminUserId, token, targetUsername, status);
        }
        public static UserResponseObj ActivateDeActivateUser(long adminUserId, string targetUsername, bool status)
        {
            return null;
            //return new UserRepository().ActivateDeActivateUser(targetUsername, status);
        }
        public static UserResponseObj ChangeFirstTimePassword(string username, string oldPassword, string newPassword, string token)
        {
            return null;
            //return new UserRepository().ChangeFirstTimePassword(username, oldPassword, newPassword, token);
        }
        public static UserResponseObj ChangeFirstTimePassword(string username, string oldPassword, string newPassword)
        {
            return null;
            //return new UserRepository().ChangeFirstTimePassword(username, oldPassword, newPassword);
        }
        public static bool ValidateUser(string username, string password, out string msg)
        {
            msg = "";
            return true;
            //return new UserRepository().ValidateUser(username, password, out msg);
        }

        public static bool IsTokenValid(long userId, string token, out string msg)
        {
            msg = "";
            return true;
            //return new UserRepository().IsTokenValid(userId, token, out msg);
        }

        public static bool IsTokenValid(long userId, string token, int transactionSource, out string msg)
        {
            msg = "";
            return true;
            //return new UserRepository().IsTokenValid(userId, token, transactionSource, out msg);
        }

        public static bool UpdateUser(User agentUser, out string msg)
        {
            msg = "";
            return true;
            //return new UserRepository().UpdateUser(agentUser, out msg);
        }
        public static UserLoginResponseObj LoginUser(string username, string password, int loginSource, string deviceSerial)
        {
            return new UserRepository().LoginUser(username, password, loginSource, deviceSerial);
        }

        public static UserResponseObj UnlockUser(long adminUserId, string targetUsername, string token)
        {
            return null;
            //return new UserRepository().UnlockUser(adminUserId, targetUsername, token);
        }

        public static UserResponseObj UnlockUser(string targetUsername)
        {
            return null;
            //return new UserRepository().UnlockUser(targetUsername);
        }

        public static UserResponseObj LockUser(long adminUserId, string targetUsername, string token)
        {
            return null;
            //return new UserRepository().LockUser(adminUserId, targetUsername, token);
        }

        public static UserResponseObj LockUser(string targetUsername)
        {
            return null;
            //return new UserRepository().LockUser(targetUsername);
        }
        public static bool IsUsernameDuplicate(string username, out string msg)
        {
            msg = "";
            return true;
            //return new UserRepository().IsUsernameDuplicate(username, out msg);
        }

        public static bool IsEmailDuplicate(string email, out string msg)
        {
            msg = "";
            return true;
            //return new UserRepository().IsEmailDuplicate(email, out msg);
        }
    }
}
