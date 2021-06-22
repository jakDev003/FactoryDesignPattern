using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FactoryDesignPattern
{

    public abstract class User
    {
        public abstract string Name { get; }
        public abstract void Process();
    }

    public class NormalUser : User
    {
        public override string Name => "normalUser";

        public override void Process()
        {
            // do something
        }
    }

    public class PowerUser : User
    {
        public override string Name => "powerUser";

        public override void Process()
        {
            // do something
        }
    }

    public static class UserFactory
    {
        private static Dictionary<string, Type> usersByName;
        private static bool IsInitialized => usersByName != null;

        private static void InitializeFactory(){
            if(IsInitialized){ return; }

            var userTypes = Assembly.GetAssembly(typeof(User)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(User)));

            // Dictionary for finding these by name later ( TODO: enum instead of string )
            usersByName = new Dictionary<string, Type>();

            // Get the names and put them in the dictionary
            foreach (var type in userTypes)
            {
                var tempUser = Activator.CreateInstance(type) as User;
                usersByName.Add(tempUser.Name, type);
            }
        }

        public static User GetUser(string userType)
        {
            InitializeFactory();

            if (usersByName.ContainsKey(userType))
            {
                Type type = usersByName[userType];
                var user = Activator.CreateInstance(type) as User;
                return user;
            }
            return null;
        }

        internal static IEnumerable<string> GetUserNames(){
            InitializeFactory();

            return usersByName.Keys;
        }
    }
}