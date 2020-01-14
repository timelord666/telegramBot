using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TelegramBot
{




    class Program
    {
       static Dictionary<string, string> questions;
        

        static void Main(string[] args)
        {

            var Api = new TelegramAPI();
            var data = System.IO.File.ReadAllText(@"C:\Users\timelord\source\repos\TelegramBot\TelegramBot\Properties\questions.json");
            questions = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

            while (true)
            {
                var updates =  Api.getUpdates();

                foreach(var up in updates)
                {
                    var answer = AnswerQuestion(up.message.text);
                    Api.sendMessage(answer, up.message.chat.id);
                    
                }

                

            }








        }

        static String AnswerQuestion(String question)
        {
            List<string> answers = new List<string>();

            question = question.ToLower();
            foreach(var q in questions)
            {
                if (question.Contains(q.Key))
                {
                    answers.Add(q.Value);
                }
            }

            if (answers.Count == 0)
            {
                answers.Add("моя твоя не понимать");
            }
        

            


         
              return  String.Join(", ", answers);
          

         



        }
    }
}
