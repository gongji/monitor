/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;

using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.Field
{
    internal class GF2Polynomial
        : IPolynomial
    {
        protected readonly int[] exponents;

        internal GF2Polynomial(int[] exponents)
        {
            this.exponents = Arrays.Clone(exponents);
        }

        public virtual int Degree
        {
            get { return exponents[exponents.Length - 1]; }
        }

        public virtual int[] GetExponentsPresent()
        {
            return Arrays.Clone(exponents);
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            GF2Polynomial other = obj as GF2Polynomial;
            if (null == other)
            {
                return false;
            }
            return Arrays.AreEqual(exponents, other.exponents);
        }

        public override int GetHashCode()
        {
            return Arrays.GetHashCode(exponents);
        }
    }
}

#endif
