using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XYZSMS.DTO;

namespace XYZSMS.Services
{
    public class SmsService : ISmsService
    {
        private readonly HttpClient _httpClient;
        private const string _baseUrl = "https://xyzsms.com/api";

        public SmsService()
        {
            _httpClient = new HttpClient(); 
        }

        public async Task<string> CheckBalance(string username, string password)
        {
            var url = $"{_baseUrl}/ApiXyzSms/CheckBalance?username={username}&password={password}&balance=true";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<MessageHistoryItem>> GetMessageHistory(string username, string password)
        {
            var url = $"{_baseUrl}/ApiXyzSms/MessageHistory?username={username}&password={password}&history=true";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<MessageHistoryItem>>(jsonString);
            }
            catch (HttpRequestException ex)
            {
                // Handle the exception appropriately
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<MessageDetails> GetMessageDetails(string username, string password, int messageId)
        {
            var url = $"{_baseUrl}/ApiXyzSms/MessageDetails?username={username}&password={password}&messageId={messageId}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<MessageDetails>(jsonString);
            }
            catch (HttpRequestException ex)
            {
                // Handle the exception appropriately
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> ComposeMessage(string username, string password, string recipients, string senderId, string smsMessage, string smsSendOption)
        {
            var url = $"{_baseUrl}/ApiXyzSms/ComposeMessage?username={username}&password={password}&recipients={recipients}&senderId={senderId}&smsmessage={smsMessage}&smssendoption={smsSendOption}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                // Handle the exception appropriately
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
