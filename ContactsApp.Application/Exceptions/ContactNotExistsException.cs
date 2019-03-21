using System;

namespace ContactsApp.Application.Exceptions
{
  public class ContactNotExistsException : BadRequestException
  {
    public ContactNotExistsException(int id) : 
      base($"The contact Id not exists: {id}")
    {
    }
  }
}