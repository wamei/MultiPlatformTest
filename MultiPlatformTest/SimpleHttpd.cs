using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace MultiPlatformTest
{
    public class SimpleHttpd
    {
        HttpListener listener;

        public SimpleHttpd(int port = 8080)
        {
            listener = new HttpListener();
            listener.Prefixes.Add($"http://127.0.0.1:{port}/");
        }

        public void Start()
        {
            listener.Start();
            listener.BeginGetContext(OnRequested, null);
        }

        private void OnRequested(IAsyncResult ar)
        {
            if (!listener.IsListening)
            {
                return;
            }

            HttpListenerContext context = listener.EndGetContext(ar);
            listener.BeginGetContext(OnRequested, listener);

            HttpListenerRequest req = context.Request;
            HttpListenerResponse res = context.Response;

            var uri = req.Url.AbsolutePath;
            uri = uri.Replace("/", ".");

            res.StatusCode = (int)HttpStatusCode.OK;

            if (uri.EndsWith(".ico"))
            {
                res.AddHeader("Content-type", "image/x-icon");
            }
            else if (uri.EndsWith(".png") || uri.EndsWith(".PNG"))
            {
                res.AddHeader("Content-type", "image/png");
            }
            else if (uri.EndsWith(".jpg") || uri.EndsWith(".JPG") || uri.EndsWith(".jpeg") || uri.EndsWith(".JPEG"))
            {
                res.AddHeader("Content-type", "image/jpeg");
            }
            else if (uri.EndsWith(".js"))
            {
                res.AddHeader("Content-type", "application/javascript");
            }
            else if (uri.EndsWith(".css"))
            {
                res.AddHeader("Content-type", "text/css");
            }
            else if (uri.EndsWith(".html") || uri.EndsWith(".htm"))
            {
                res.AddHeader("Content-type", "text/html");
            }
            else if (uri.EndsWith(".map"))
            {
                res.AddHeader("Content-type", "application/json");
            }
            else
            {
                res.AddHeader("Content-type", "text/plain");
            }

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(SimpleHttpd)).Assembly;
            Stream stream = assembly.GetManifestResourceStream($"MultiPlatformTest.Resources{uri}");
            stream.CopyTo(res.OutputStream);

            res.Close();
        }
    }
}
