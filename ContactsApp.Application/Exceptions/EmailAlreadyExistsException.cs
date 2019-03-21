using System;

namespace ContactsApp.Application.Exceptions
{
  public class EmailAlreadyExistsException : BadRequestException
  {
    public EmailAlreadyExistsException(string email) : 
      base($"The e-mail already exists on database: {email}")
    {
    }
  }
}