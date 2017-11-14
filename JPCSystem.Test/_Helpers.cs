using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Web;

namespace JPCSystem.Test
{
    public static class _Helpers
    {
        public static HttpContextBase GetFakeContext()
        {
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                new System.Net.WebHeaderCollection {
                    {"X-Requested-With", "XMLHttpRequest"}
                });

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            return context.Object;
        }
    }
}
