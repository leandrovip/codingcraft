using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;

namespace CodingCraftEx04.Api.Providers
{
    public class MediaStreamProvider
    {
        private readonly string _nomeDoArquivo;

        public MediaStreamProvider(string nomeDoArquivo)
        {
            _nomeDoArquivo = nomeDoArquivo;
        }

        public async void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                var buffer = new byte[65536];

                using (var media = File.OpenRead(_nomeDoArquivo)) //, FileMode.Open, FileAccess.ReadWrite))
                {
                    var length = (int) media.Length;
                    var bytesRead = 1;

                    while (length > 0 && bytesRead > 0)
                    {
                        bytesRead = media.Read(buffer, 0, Math.Min(length, buffer.Length));
                        await outputStream.WriteAsync(buffer, 0, bytesRead);
                        length -= bytesRead;
                    }
                }
            }
            catch (HttpException ex)
            {
            }
            finally
            {
                outputStream.Close();
            }
        }
    }

    public class VideoStream : MediaStreamProvider
    {
        public VideoStream(string nomeDoArquivo): base(nomeDoArquivo)
        { }
    }

    public class AudioStream : MediaStreamProvider
    {
        public AudioStream (string nomeDoArquivo): base(nomeDoArquivo)
        { }
    }
}