
namespace George.Control.Layer
{
    using George.Services.Layer.AudioService;

    public  class AudioController
    {
        #region Instances
        private object _systemVocabulary;
        private readonly TextToSpeech _textToSpeech;
        private readonly SpeechRecognition _speechRecognition;
        #endregion

        public AudioController()
        {
            _systemVocabulary = new object();
            _textToSpeech = new TextToSpeech(); 
            _speechRecognition = new SpeechRecognition();
        }

        #region Text-to-Speech Methods
        public void Speak(string setting)
        {
            _textToSpeech.Speak(setting);
        }
        #endregion

    }
}
