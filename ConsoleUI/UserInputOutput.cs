namespace ConsoleUI
{
    using System;

    public static class UserInputOutput
    {
        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        public static void ClearScreen()
        {
            Console.Clear();
        }

        public static void WriteLine(params string[] i_ContainerOfStr)
        {
            foreach (string str in i_ContainerOfStr)
            {
                Console.WriteLine(str);
            }
        }

        public static void Write(params string[] i_ContainerOfStr)
        {
            foreach (string str in i_ContainerOfStr)
            {
                Console.Write(str);
            }
        }

        public static void NewLine()
        {
            Console.WriteLine(Environment.NewLine);
        }
    }
}