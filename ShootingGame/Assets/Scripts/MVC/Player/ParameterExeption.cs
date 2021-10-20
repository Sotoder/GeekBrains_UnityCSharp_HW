using System;

namespace Model.ShootingGame
{
    public sealed class ParameterException : Exception
    {
        public int Parameter { get; }
        public ParameterException(string message, int value) : base(message)
        {
            Parameter = value;
        }
    }
}
