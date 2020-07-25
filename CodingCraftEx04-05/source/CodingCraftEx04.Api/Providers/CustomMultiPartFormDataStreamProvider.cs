using System.Net.Http;
using System.Net.Http.Headers;

namespace CodingCraftEx04.Api.Providers
{
    public class CustomMultiPartFormDataStreamProvider :  MultipartFormDataStreamProvider
    {
        public CustomMultiPartFormDataStreamProvider(string rootPath) : base(rootPath)
        {
        }

        public CustomMultiPartFormDataStreamProvider(string rootPath, int bufferSize) : base(rootPath, bufferSize)
        {
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            return headers.ContentDisposition.FileName.Replace("\"", string.Empty);
        }
    }
}