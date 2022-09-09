// See https://aka.ms/new-console-template for more 

using George.Services.Layer.AudioService;

SpeechRecognition speechRecognition = new SpeechRecognition();

speechRecognition.StartDictating();

while (true)
{
    Console.WriteLine("Keep program running: " + speechRecognition.GetCommandTextResult());
}



