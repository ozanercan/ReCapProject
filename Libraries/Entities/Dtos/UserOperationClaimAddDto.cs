using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserOperationClaimAddDto : IDto
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
