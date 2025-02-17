using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Models;
using DataAccess;

namespace Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PaymentRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        // ✅ 1. Lấy thông tin thanh toán theo transactionId
        public async Task<Payment?> GetPaymentByTransactionIdAsync(string transactionId)
        {
            return await PaymentDAO.Instance.GetPaymentByTransactionIdAsync(transactionId);
        }

        // ✅ 2. Lấy danh sách thanh toán của người dùng
        public async Task<List<Payment>> GetPaymentsByUserAsync(int userId)
        {
            return await PaymentDAO.Instance.GetPaymentsByUserAsync(userId);
        }

        // ✅ 3. Xử lý thanh toán (lưu transaction vào database)
        public async Task<bool> ProcessPaymentAsync(int cartId, string paymentMethod, string transactionId)
        {
            return await PaymentDAO.Instance.ProcessPaymentAsync(cartId, paymentMethod, transactionId);
        }

        // ✅ 4. Gửi yêu cầu thanh toán đến PayOS
        public async Task<string> CreatePaymentRequest(int userId, decimal amount)
        {
            var clientId = _configuration["PayOS:ClientId"];
            var apiKey = _configuration["PayOS:ApiKey"];
            var checksumKey = _configuration["PayOS:ChecksumKey"];

            // Chuyển DateTime thành ticks và đảm bảo giá trị đủ lớn
            long orderCode = DateTime.UtcNow.Ticks;
            int orderCodeInt = (int)orderCode;

            var payload = new Dictionary<string, object>
    {
        { "orderCode", orderCodeInt.ToString() },  // Sử dụng ticks của DateTime
        { "amount", amount },
        { "description", "Thanh toán đơn hàng từ hệ thống" },
        { "returnUrl", $"https://localhost:7122/PaymentCallback?orderId={orderCodeInt}&status=success" },
        { "cancelUrl", $"https://localhost:7122/PaymentCallback?orderId={orderCodeInt}&status=failed" }
    };

            // Tạo chữ ký (signature)
            var signature = GenerateSignature(payload, checksumKey);
            payload.Add("signature", signature);

            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api-merchant.payos.vn/v2/payment-requests")
            {
                Content = content
            };

            // Thêm header xác thực
            request.Headers.Add("x-client-id", clientId);
            request.Headers.Add("x-api-key", apiKey);

            try
            {
                Console.WriteLine($"🔹 Sending request to PayOS: {jsonPayload}");
                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"🔹 PayOS Response: {response.StatusCode} - {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"❌ Error from PayOS: {response.StatusCode}");
                    return null;
                }

                var responseObject = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);
                return responseObject != null && responseObject.ContainsKey("checkoutUrl") ? responseObject["checkoutUrl"].ToString() : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Exception when calling PayOS: {ex.Message}");
                return null;
            }
        }




        private string GenerateSignature(Dictionary<string, object> payload, string checksumKey)
        {
            var sortedParams = new SortedDictionary<string, object>(payload);
            var dataToSign = new StringBuilder();
            foreach (var kvp in sortedParams)
            {
                if (dataToSign.Length > 0)
                    dataToSign.Append("&");
                dataToSign.Append($"{kvp.Key}={kvp.Value}");
            }

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(checksumKey)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataToSign.ToString()));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
