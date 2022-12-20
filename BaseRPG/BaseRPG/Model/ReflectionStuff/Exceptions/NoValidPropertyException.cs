using System;
using System.Runtime.Serialization;

namespace BaseRPG.Model.ReflectionStuff.Exceptions
{
    [Serializable]
    internal class NoValidPropertyException : Exception
    {
        private Type type;

        public NoValidPropertyException()
        {
        }

        public NoValidPropertyException(Type type):this("No property found that matches the type "+type.Name)
        {
            
        }

        public NoValidPropertyException(string message) : base(message)
        {
        }

        public NoValidPropertyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoValidPropertyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}