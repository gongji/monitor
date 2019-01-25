/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	public sealed class EnumerableProxy
		: IEnumerable
	{
		private readonly IEnumerable inner;

		public EnumerableProxy(
			IEnumerable inner)
		{
			if (inner == null)
				throw new ArgumentNullException("inner");

			this.inner = inner;
		}

		public IEnumerator GetEnumerator()
		{
			return inner.GetEnumerator();
		}
	}
}

#endif
