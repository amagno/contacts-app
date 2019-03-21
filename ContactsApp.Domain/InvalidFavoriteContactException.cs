using System;

namespace ContactsApp.Domain
{
  public class InvalidFavoriteContactException : Exception
  {
    public InvalidFavoriteContactException(int age) : 
      base($"The age for favorite contact is invalid: {age}")
    {
    }
    public InvalidFavoriteContactException(string message) : base(message)
    {
    }
  }
}