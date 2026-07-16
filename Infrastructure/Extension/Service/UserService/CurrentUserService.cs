using Application.Interfaces.Services.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
namespace Infrastructure.Extension.Service.UserService
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int UserId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?
                    .User?
                    .FindFirst("UserId")?
                    .Value;

                return string.IsNullOrEmpty(value) ? 0 : int.Parse(value);
            }
        }
    }
}
