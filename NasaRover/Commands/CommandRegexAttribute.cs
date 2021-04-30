using System;
using System.Text.RegularExpressions;

namespace NasaRover.Commands
{
	public class CommandRegexAttribute : Attribute
	{
		public CommandRegexAttribute(string pattern)
		{
			Regex = new Regex(pattern);
		}

		public Regex Regex { get; private set; }
	}
}