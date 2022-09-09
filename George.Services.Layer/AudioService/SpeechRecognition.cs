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

        #region Launch Methods
        public void StartListening()
        {
            _commandSpeechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void StartDictating()
        {
            _commandSpeechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }
        #endregion

        #region Getter methods
        public string GetCommandTextResult()
        {
            var result = _commandTextResult;
            _commandTextResult = "";

            return result;
        }
        public string GetDicattionTextResult()
        {
            var result = _dicatationTextResult;
            _dicatationTextResult = "";

            return result;
        }
        #endregion

        #region Initialization Methods
        private void LoadSystemVocabulary()
        {
            //Get commands
            var commandChoices = new Choices(Vocabulary.GetCommands("Sign-up: Exit"));
            
            //Prepare recognizer grammar
            var commandGrammar = new Grammar(new GrammarBuilder(commandChoices));
            var dicatationGrammar = new DictationGrammar();

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
        
        #region Event Handlers Methods
        private void _commandRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)  
        {
            _commandTextResult = e.Result.Text;
            Debug.WriteLine("Recognized text: " + _commandTextResult);  
        }
        private void _dictationSpeechRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            _dicatationTextResult = e.Result.Text;
            Debug.WriteLine("Recognized text: " + _dicatationTextResult);
        }
        #endregion

    }
}

#pragma warning restore CA1416 // Validate platform compatibility
