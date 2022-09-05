// See https://aka.ms/new-console-template for more 

using George.Services.Layer.AudioService;
using George.Services.Layer.EncryptionService;


TextToSpeech speak = new TextToSpeech();
speak.Speak("I cut my leeeeeeeeeeeee ieeeep!");
speak.Speak(Vocabulary.GetPromptMessage("Sign-up: Introduction"));