using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password); //sisteme kayıt 
        IDataResult<User> Login(UserForLoginDto userForLoginDto); //sisteme giriş 
        IResult UserExists(string email); //kuallnıcı varmı ? 
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
