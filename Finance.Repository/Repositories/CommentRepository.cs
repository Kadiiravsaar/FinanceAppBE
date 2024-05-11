using Finance.Core.Models;
using Finance.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Repository.Repositories
{
	public class CommentRepository : GenericRepository<Comment>, ICommentRepository
	{
		public CommentRepository(AppDbContext context) : base(context)
		{
		}

	}


	

}
