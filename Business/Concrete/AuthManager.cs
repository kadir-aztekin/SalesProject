using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService //kullanıcı varmı yokmu register veya login olayı
    {
        private IUserService _userService; //kullanıcı kontrolu için 
        private ITokenHelper _tokenHelper; //kullanıcı login oldugunda ona token vermemız lazım 

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password) //yeni kullanıcı ekleme
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User //yeni kullanıcı ekleme  
            //zaten dto tablosunda bız propları tanımladık 
            {
                Email = userForRegisterDto.Email,   
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto) //öncedn kayıt olmusları test edıyoruz varmı yokmu
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email); //kullanıcı var mı?
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            //şifre varmı ?
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin); //yukardan geçtiyse hepsı var 
        }

        public IResult UserExists(string email) //kullanıcı varmı 
        {
            if (_userService.GetByMail(email) != null) //boş degıl demekkı yanı oncedeen bu kullanıcı mevcut 
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user) //kullanıcı kayıt olduktan sonra  veya login 
                                                                     // kullanıcya token verecegız ve gırıs cıkısı token la yapcak 
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
