using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Account
{
	public class AttributeValue
	{
		AttributeValue()
		{
		}

		AttributeValue(double value)
		{
			@base=value;
			modified=value;
		}

		double @base;     /**< Base value of the attribute. */
		double modified; /**< Value after various modifiers have been applied. */
	}
}
