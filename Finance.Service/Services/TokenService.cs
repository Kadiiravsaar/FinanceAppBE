﻿using Finance.Core.Models;
using Finance.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Service.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;
		private readonly SymmetricSecurityKey _key;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
		}
		public string CreateToken(AppUser appUser)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.NameId, appUser.Id),
				new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
				new Claim(JwtRegisteredClaimNames.GivenName, appUser.UserName)
			};
			var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescription = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = creds,
				Issuer = _configuration["JWT:Issuer"],
				Audience = _configuration["JWT:Audience"]
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescription);

			return tokenHandler.WriteToken(token);

		}
	}
}