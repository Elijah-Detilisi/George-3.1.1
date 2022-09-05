
namespace George.Control.Layer
{
    using George.Services.Layer.AudioService;

    public  class AudioController
    {
        #region Instances
        private readonly TextToSpeech _textToSpeech;
        private readonly SpeechRecognition _speechRecognition;
        #endregion

        public AudioController()
        {
            _textToSpeech = new TextToSpeech(); 
            _speechRecognition = new SpeechRecognition();
        }

        #region Text-to-Speech Methods
        public void Speak(string promptKey)
        {
            string promptMessage = Vocabulary.GetPromptMessage(promptKey);
            _textToSpeech.Speak(promptMessage);
        }
        #endregion

        #region Speech-recognition Methods
        public string Listen()
        {
            return _speechRecognition.Listen();
        }
        public string Dictate()
        {
            return _speechRecognition.Dictate();
        }
        #endregion

    }
}
