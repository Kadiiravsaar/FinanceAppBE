﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.Models
{
	public class BaseEntity
	{
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
		public bool IsDeleted { get; set; } = false;
		public DateTime? UpdatedDate { get; set; }
	}
}
