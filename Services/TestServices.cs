using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furion.DependencyInjection;

namespace WebApplication.Services
{
	public class TestServices:ITransient
	{

		public string getHello() {
			return @"helloWord";
		}
	}
}
