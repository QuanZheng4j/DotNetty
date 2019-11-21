using System;

namespace DotNetty
{

    /// <summary>
    /// A class for checkings of preconditions.
    /// </summary>
    public sealed class Preconditions
    {
        private Preconditions() { }

        public static Boolean CheckNotNull(Object reference, Object errorMessage)
        {
            if (reference == null)
            {
                //throw new NullReferenceException(errorMessage.ToString());
                System.Console.WriteLine(errorMessage.ToString());
                //change to "null" if refernce of message is null.
                return false;
            }
            else
            {
                return true;
            }
        }
        public static Boolean CheckNotNullArgument(Object reference, Object errorMessage)
        {
            if (reference == null)
            {
                // throw new ArgumentNullException(errorMessage.ToString());
                System.Console.WriteLine(errorMessage.ToString());
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Boolean CheckArgument(Boolean expression, Object errorMessage)
        {
            if (!expression)
            {
                //throw new ArgumentException(errorMessage.ToString());
                // System.Console.WriteLine(errorMessage.ToString());
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}