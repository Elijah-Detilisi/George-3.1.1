using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace George.Services.Layer.AudioService
{
    using System.Speech.Recognition;

#pragma warning disable CA1416 // Validate platform compatibility

    public class SpeechRecognition
    {
        #region Instances
        private readonly SpeechRecognitionEngine _speechRecognitionEngine;
        #endregion

        public SpeechRecognition()
        {
            var language = new System.Globalization.CultureInfo("en-us");
            _speechRecognitionEngine = new SpeechRecognitionEngine(language);
            LoadRecognizer();
        }

        #region Class Methods
        public string Listen()
        {
            Console.WriteLine("Listening...");
            try
            {
                RecognitionResult result = _speechRecognitionEngine.Recognize();
                return result.Text;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Didn't get that, please try again.";
            }
        }
        #endregion

        #region Initialization Methods
        private Choices GetChoices()
        {
            var myChoices = new Choices();

            //inbox actions
            myChoices.Add("exit");
            myChoices.Add("read");
            myChoices.Add("write");
            myChoices.Add("help");
            myChoices.Add("something greater");

            return myChoices;
        }

        private void LoadRecognizer()
        {
            var grammarBuilder = new GrammarBuilder(GetChoices());
            var grammar = new Grammar(grammarBuilder);

            _speechRecognitionEngine.LoadGrammar(grammar);
            _speechRecognitionEngine.SetInputToDefaultAudioDevice();
        }
        #endregion
    }

#pragma warning restore CA1416 // Validate platform compatibility

}
