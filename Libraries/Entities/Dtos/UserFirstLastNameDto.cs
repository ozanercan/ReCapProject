using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserFirstLastNameDto : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
