using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMS_Repository.Extensions
{
   public static class IdentityServiceExtensions
    {
           public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            // Add the authentication services options hear

            return services;
        }
    }
}
