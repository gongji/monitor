/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;

namespace Org.BouncyCastle.Security.Certificates
{
#if !(NETCF_1_0 || NETCF_2_0 || SILVERLIGHT || NETFX_CORE)
    [Serializable]
#endif
    public class CertificateEncodingException : CertificateException
	{
		public CertificateEncodingException() : base() { }
		public CertificateEncodingException(string msg) : base(msg) { }
		public CertificateEncodingException(string msg, Exception e) : base(msg, e) { }
	}
}

#endif
