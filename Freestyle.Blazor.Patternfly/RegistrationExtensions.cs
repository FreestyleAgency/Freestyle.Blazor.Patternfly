using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freestyle.Blazor.Patternfly
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddPatternfly(this IServiceCollection services)
		{
			services.AddTransient<IOutsideClickListener, OutsideClickListener>();
			services.AddScoped<IJSFunctions, JSFunctions>();

			return services;
		}
	}
}
