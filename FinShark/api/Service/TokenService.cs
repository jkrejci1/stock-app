using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        //ctor --> construct
        public TokenService(IConfiguration config) //IConfig goes into the app settings json
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"])); //Encrypts in a unique way specific to our server so people can't mess with the token
        }
        public string CreateToken(AppUser user)
        {
            //Put the claims within our tokens (like email, username, timezone, etc)
            //Used to identify the user and express what they can and can't do like a role
            var claims = new List<Claim>
            {
                //Given to our claims
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            //Create the signing credentials (type of encrytption we want to use)
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //Create the token as an object and .net will create the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //The ClaimsIdentity is like our wallet where we put the important things
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7), //Creates an expiration date for the token so it doesn't stay active forever
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"], //Gets the issuer from the json app settings file (may need to change that stuff when deploying to the internet)
                Audience = _config["JWT:Audience"] //Audience is just like getting issuer above
            };

            //Next, we need to use the token handler to create the actual token for us
            var tokenHandler = new JwtSecurityTokenHandler();

            //Save our token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            //Return the token in the form of a string
            return tokenHandler.WriteToken(token);
        }
    }
}