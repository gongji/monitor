/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;

namespace Org.BouncyCastle.Crypto.Tls
{
    public class SupplementalDataEntry
    {
        protected readonly int mDataType;
        protected readonly byte[] mData;

        public SupplementalDataEntry(int dataType, byte[] data)
        {
            this.mDataType = dataType;
            this.mData = data;
        }

        public virtual int DataType
        {
            get { return mDataType; }
        }

        public virtual byte[] Data
        {
            get { return mData; }
        }
    }
}

#endif
