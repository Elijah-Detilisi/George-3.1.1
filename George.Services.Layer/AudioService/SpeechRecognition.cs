using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CA1416 // Validate platform compatibility

namespace George.Services.Layer.AudioService
{
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
            _commandTextResult = "";
            _recognizerDialact = new CultureInfo("en-US");
            _commandSpeechRecognizer = new SpeechRecognitionEngine(_recognizerDialact);
            _dictationSpeechRecognizer = new SpeechRecognitionEngine(_recognizerDialact);

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
            //Prepare recognizer grammar
            var grammarBuilder = new GrammarBuilder(GetChoices());
            var commandGrammar = new Grammar(grammarBuilder);
            var dicatationGrammar = new DictationGrammar();

            _commandSpeechRecognizer.LoadGrammar(commandGrammar);
            _dictationSpeechRecognizer.LoadGrammar(dicatationGrammar);
            _commandSpeechRecognizer.SetInputToDefaultAudioDevice();
            _dictationSpeechRecognizer.SetInputToDefaultAudioDevice();
            
            //Speech event Handlers
            _commandSpeechRecognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_commandRecognizer_SpeechRecognized);
            _commandSpeechRecognizer.RecognizeAsync(RecognizeMode.Multiple); 

        } 
        #endregion
        
        #region Event Handlers
        private void _commandRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)  
        {
            _commandTextResult = e.Result.Text;
            Console.WriteLine("Recognized text: " + _commandTextResult);  
        } 
        #endregion
        
        #region Getter methods
        public string GetCommandTextResult(){
            var result = _commandTextResult;
            _commandTextResult = "";
            
            return result;
        }
        #endregion
    }
}

#pragma warning restore CA1416 // Validate platform compatibility
