using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Core.Services;
using Finance.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Service.Services
{
	public class CommentService : Service<Comment>, ICommentService
	{
		public CommentService(IGenericRepository<Comment> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
		{
		}
	}
}
