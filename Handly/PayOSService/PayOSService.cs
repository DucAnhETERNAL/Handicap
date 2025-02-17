using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class PayOSService
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _apiKey;
    private readonly string _checksumKey;

    public PayOSService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientId = configuration["PayOS:ClientId"];
        _apiKey = configuration["PayOS:ApiKey"];
        _checksumKey = configuration["PayOS:ChecksumKey"];
    }

    public async Task<string> CreatePaymentOrder(decimal amount, string orderInfo, string returnUrl)
    {
        var requestUrl = "https://api.payos.vn/v2/payment-requests"; // API của PayOS
        var requestData = new
        {
            client_id = _clientId,
            amount = amount,
            description = orderInfo,
            return_url = returnUrl,
            notify_url = "https://yourdomain.com/api/payment/callback" // URL nhận callback từ PayOS
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
        jsonContent.Headers.Add("x-client-id", _clientId);
        jsonContent.Headers.Add("x-api-key", _apiKey);

        var response = await _httpClient.PostAsync(requestUrl, jsonContent);
        var responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error from PayOS API: {responseString}");
        }

        dynamic responseObject = JsonConvert.DeserializeObject(responseString);
        return responseObject.checkout_url; // Trả về URL thanh toán
    }
}
