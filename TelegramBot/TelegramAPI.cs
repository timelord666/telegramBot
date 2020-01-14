using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace TelegramBot
{


    class Chat
    {
        public int id { get; set; }
        public string first_name { get; set; }
    }

    class Message
    {
       public Chat chat { get; set; }
        public string text { get; set; }
    }

    class Update
    {
        public int update_id { get; set; }
        public Message message { get; set; }
    }


    class ApiResult
    {
        public Update[] result { get; set; }
    }




    class TelegramAPI
    {
        const String API_KEY = "877006364:AAGavRTDRBIzOoxOS3GRaBWVJ9E0Wsui_CQ";
        const String API_URL = "https://api.telegram.org/bot" + API_KEY + "/";

        RestClient RC = new RestClient();

        public TelegramAPI()
        {

        }

        public Update[] getUpdates()
        {
            var json = sendApiRequest("getUpdates", "offset=" + lastUpdateId);
            var updates = JsonConvert.DeserializeObject<ApiResult>(json);

            foreach(var result in updates.result)
            {
                Console.WriteLine("recievd update" + result.update_id + "message from  " + result.message.chat.first_name + "Text: "  + result.message.text);
                lastUpdateId = result.update_id + 1;
            }
            return updates.result;
        }


        public void sendMessage(String text, int id)
        {
            sendApiRequest("sendMessage", $"chat_id={id}&text={text}");
            
        }


        public String sendApiRequest(String ApiMethod)
        {
            var url = API_URL + ApiMethod;
            var request = new RestRequest(url);
            var response = RC.Get(request);

            return response.Content;
        }


        public String sendApiRequest(String ApiMethod, String parametrs)
        {
            var url = API_URL + ApiMethod + "?" + parametrs;
            var request = new RestRequest(url);
            var response = RC.Get(request);
            

            return response.Content;
        }

        private int lastUpdateId = 0;

    }
}
