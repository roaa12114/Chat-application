using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace AllChatClient
{
    public class UserAuthenticationManager
    {
        public static string filePath = @"F:\semester 4\telecommunication\AllChat-main\AllChatClient\AllChatClient\Resources\UserAuthentication.txt";
        public static void persist(UserAuthentication userAuthentication)
        {
            List<string> newUser = new List<string>();
            newUser.Add(userAuthentication.username + " " + userAuthentication.password);
            File.AppendAllLines(filePath, newUser);
        }

        public static List<UserAuthentication> readAllData()
        {
            List<string> userList = File.ReadAllLines(filePath).ToList();
            List<UserAuthentication> users = new List<UserAuthentication>();
            foreach (string user in userList)
            {
                UserAuthentication userAuthentication = new UserAuthentication();
                userAuthentication.username = user.Split(' ')[0];
                userAuthentication.password = user.Split(' ')[1];
                users.Add(userAuthentication);

            }

            return users;
        }

        public static bool loginUserExists(UserAuthentication userAuthentication)
        {
            List<UserAuthentication> users = readAllData();
            foreach (UserAuthentication user in users)
            {
                if (user.username.Equals(userAuthentication.username))
                {
                    if (user.password.Equals(userAuthentication.password))
                    {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public static bool registerUserExists(UserAuthentication userAuthentication)
        {
            List<UserAuthentication> users = readAllData();
            foreach (UserAuthentication user in users)
            {
                if (user.username.Equals(userAuthentication.username))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
