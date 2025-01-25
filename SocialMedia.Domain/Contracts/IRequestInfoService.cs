using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Contracts
{
    public interface IRequestInfoService
    {
        string? CorrelationId { get; set; }
        Guid? UserId { get; }
        string? AccessToken { get; }
    }
}
