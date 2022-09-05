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
        private readonly SpeechRecognitionEngine _commandSpeechRecognizer;
        private readonly SpeechRecognitionEngine _dictationSpeechRecognizer;
        #endregion

        public SpeechRecognition()
        {
            var language = new System.Globalization.CultureInfo("en-uk");
            _commandSpeechRecognizer = new SpeechRecognitionEngine(language);
            _dictationSpeechRecognizer = new SpeechRecognitionEngine(language);

            LoadRecognizer();
        }

        #region Class Methods
        public string Listen()
        {
            try
            {
                RecognitionResult result = _commandSpeechRecognizer.Recognize();
                if (result != null)
                {
                    return result.Text;
                }
                else
                {
                    return "Didn't get that, please try again.";
                }
            }
            catch (Exception)
            {
               return "Didn't get that, please try again.";
            }
        }

        public string Dictate()
        {
            try
            {
                RecognitionResult result = _dictationSpeechRecognizer.Recognize();
                return result.Text;
            }
            catch (Exception)
            {
                return "Didn't get that, please try again.";
            }
        }
        #endregion

        #region Initialization Methods
        private Choices GetChoices()
        {
            var myChoices = new Choices();

            foreach(var command in Vocabulary.GetCommands("Sign-up: Exit"))
            {
                myChoices.Add(command);
            }

            return myChoices;
        }

        private void LoadRecognizer()
        {
            var grammarBuilder = new GrammarBuilder(GetChoices());
            var grammar = new Grammar(grammarBuilder);

            _commandSpeechRecognizer.LoadGrammar(grammar);
            _commandSpeechRecognizer.SetInputToDefaultAudioDevice();
        }
        #endregion
    }

#pragma warning restore CA1416 // Validate platform compatibility

}
