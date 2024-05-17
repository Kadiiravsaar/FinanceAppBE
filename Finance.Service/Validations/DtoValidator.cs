using Finance.Core.DTOs.Stock;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Service.Validations
{
	public class DtoValidator : AbstractValidator<CreateStockRequestDto>
	{
		public DtoValidator()
		{
			RuleFor(x => x.Symbol).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
			RuleFor(x => x.CompanyName).NotNull().WithMessage("{PropertyName} is required");
			RuleFor(x => x.Purchase).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0");
			RuleFor(x => x.LastDiv).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0");
		}
	}
}
