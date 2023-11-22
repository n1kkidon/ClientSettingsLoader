using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

public class HttpConnection
{
    private readonly HttpClient HTTP_CLIENT;
    private readonly string _port;
    public HttpConnection(string port, string authToken)
    {
        try
        {
            HTTP_CLIENT = new HttpClient(new HttpClientHandler()
            {
                SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls,
                ServerCertificateCustomValidationCallback = (a, b, c, d) => true
            });
        }
        catch
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HTTP_CLIENT = new HttpClient(new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (a, b, c, d) => true
            });
        }
        _port = port;
        var byteArray = Encoding.ASCII.GetBytes("riot:" + authToken);
        var token = Convert.ToBase64String(byteArray);
        HTTP_CLIENT.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
    }

    public async Task<string> Get(string url)
    {
        var res = await HTTP_CLIENT.GetAsync("https://127.0.0.1:" + _port + url);
        var stringContent = await res.Content.ReadAsStringAsync();
        return stringContent;
    }
    public async Task<HttpResponseMessage> Post(string url, string body)
    {
        return await HTTP_CLIENT.PostAsync("https://127.0.0.1:" + _port + url, new StringContent(body, Encoding.UTF8, "application/json"));
    }

    public async Task<HttpResponseMessage> Put(string url, string body)
    {
        return await HTTP_CLIENT.PutAsync("https://127.0.0.1:" + _port + url, new StringContent(body, Encoding.UTF8, "application/json"));
    }

    public async Task<HttpResponseMessage> Patch(string url, string body)
    {
        return await HTTP_CLIENT.PatchAsync("https://127.0.0.1:" + _port + url, new StringContent(body, Encoding.UTF8, "application/json"));
    }
}