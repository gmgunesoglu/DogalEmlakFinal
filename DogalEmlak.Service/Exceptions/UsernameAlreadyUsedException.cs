using System.Runtime.Serialization;

namespace DogalEmlak.Service.Exceptions
{
    [Serializable]
    public class UsernameAlreadyUsedException : Exception
    {
        public UsernameAlreadyUsedException() : base("This user name is allready used!")
        {
        }

        public UsernameAlreadyUsedException(string str) : base(str)
        {
        }
    }
}