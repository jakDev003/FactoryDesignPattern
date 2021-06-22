using System;

namespace FactoryDesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(string name in UserFactory.GetUserNames()){
                Console.WriteLine(name);
            }
        }
    }
}
