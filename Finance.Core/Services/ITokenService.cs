using Finance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.Services
{
	public interface ITokenService
	{
		string CreateToken(AppUser appUser);
	}
}
