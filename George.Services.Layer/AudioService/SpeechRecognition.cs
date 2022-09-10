using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CA1416 // Validate platform compatibility

namespace George.Services.Layer.AudioService
{
    using System.Diagnostics;
    using System.Globalization;
    using System.Speech.Recognition;

    public class SpeechRecognition
    {
        #region Instances
        private string _commandTextResult;
        private string _dicatationTextResult;
        private CultureInfo _recognizerDialact;
        private readonly SpeechRecognitionEngine _commandSpeechRecognizer;
        private readonly SpeechRecognitionEngine _dictationSpeechRecognizer;
        
        #endregion

        public SpeechRecognition()
        {
            _commandTextResult = "";
            _dicatationTextResult = "";
            _recognizerDialact = new CultureInfo("en-GB");
            _commandSpeechRecognizer = new SpeechRecognitionEngine(_recognizerDialact);
            _dictationSpeechRecognizer = new SpeechRecognitionEngine(_recognizerDialact);

            initializeRecognizer();
        }

        #region Recognizer Launching
        public void StartListening()
        {
            _commandSpeechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void StartDictating()
        {
            _dictationSpeechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }
        #endregion

        #region Recognizer Results
        public string GetCommand()
        {
            var result = _commandTextResult;
            _commandTextResult = "";

            return result;
        }
        public string GetAsyncDictation()
        {
            var result = _dicatationTextResult;
            _dicatationTextResult = "";

            return result;
        }
        public string GetActiveDictation()
        {
            var result = _dictationSpeechRecognizer.Recognize().Text;

            return (result != null) ? result : "";
        }
        #endregion

        #region Recognizer Initialization
        private void LoadSystemVocabulary()
        {
            //Get commands
            var commandVocabulary = Vocabulary.GetCommands("Sign-up: Exit").Concat(
                                    Vocabulary.GetCommands("Sign-up: Exit"));

            var dictationVocabulary = Vocabulary.GetDictationInputs("Spelling: AlphaNumerics").Concat(
                                      Vocabulary.GetDictationInputs("Email: Domains"));

            var commandChoices = new Choices(commandVocabulary.ToArray<string>());
            var dictationChoices = new Choices(dictationVocabulary.ToArray<string>());

            //Prepare recognizer grammar
            var commandGrammar = new Grammar(new GrammarBuilder(commandChoices));
            var dicatationGrammar = new Grammar(new GrammarBuilder(dictationChoices));

            //Load Grammar
            _commandSpeechRecognizer.LoadGrammar(commandGrammar);
            _dictationSpeechRecognizer.LoadGrammar(dicatationGrammar);

        }

        private void initializeRecognizer()
        {
            LoadSystemVocabulary();
            _commandSpeechRecognizer.SetInputToDefaultAudioDevice();
            _dictationSpeechRecognizer.SetInputToDefaultAudioDevice();
            
            //Speech event Handlers
            _commandSpeechRecognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_commandRecognizer_SpeechRecognized);
            _dictationSpeechRecognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_dictationSpeechRecognizer_SpeechRecognized);
        } 
        #endregion
        
        #region Recognizer Event Handlers
        private void _commandRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)  
        {
            _commandTextResult = e.Result.Text;
            Debug.WriteLine("Recognized command: " + _commandTextResult);  
        }
        private void _dictationSpeechRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            _dicatationTextResult = e.Result.Text;
            Debug.WriteLine("Recognized dictation: " + _dicatationTextResult);
        }
        #endregion

    }
}

#pragma warning restore CA1416 // Validate platform compatibility
