using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CA1416 // Validate platform compatibility

namespace George.Services.Layer.AudioService
{
    using System.Speech.Recognition;



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
            RecognitionResult result = _commandSpeechRecognizer.Recognize();
            if (result != null)
            {
                return result.Text;
            }
            else
            {
                throw new Exception("Failed To Recognize");
            }
        }

        public string Dictate()
        {
            RecognitionResult result = _dictationSpeechRecognizer.Recognize();
            if (result != null)
            {
                return result.Text;
            }
            else
            {
                throw new Exception("Failed To Dictate");
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
            _dictationSpeechRecognizer.SetInputToDefaultAudioDevice();
        }
        #endregion
    }
}

#pragma warning restore CA1416 // Validate platform compatibility
