// See https://aka.ms/new-console-template for more 

using George.Services.Layer.AudioService;

SpeechRecognition speechRecognition = new SpeechRecognition();
/*speechRecognition.StartListening();

while (true)
{
    Console.WriteLine("Keep program running: " + speechRecognition.GetDicattionTextResult());
}*/


var temp = new Dictionary<string, string[]>()
        {
            {
                "Spelling: AlphaNumerics",
                    "a-b-c-d-e-f-g-h-i-j-k-l-m-n-o-p-q-r-s-t-u-v-w-x-y-z-1-2-3-4-5-6-7-8-9-0".Split("-")
            },
            {
                "Email: Domains",
                    new string[]{
                        "@gmail.com", "@yahoo.com", "@hotmail.com",
                        "@outlook.com", "@office365.com"
                    }
            },

        };
var temp2 = temp.Keys;

Console.WriteLine(temp2);



