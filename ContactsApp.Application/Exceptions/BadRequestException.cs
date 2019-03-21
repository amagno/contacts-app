using System;

namespace ContactsApp.Application.Exceptions
{
  public class BadRequestException : Exception
  {
    public BadRequestException(string message) : base(message)
    {
    }
  }
}