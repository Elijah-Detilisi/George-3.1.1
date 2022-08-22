﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace George.Services.Layer.AudioService
{
    using System.Speech.Synthesis;

    public class TextToSpeech
    {
        #region Instances
        private readonly SpeechSynthesizer _speechSynthesizer;
        #endregion

        public TextToSpeech()
        {
            _speechSynthesizer = new SpeechSynthesizer();
        }

        #region Setter Methods
        public void SetAudioSpeed(int rate)
        {
            _speechSynthesizer.Rate = rate;
        }

        public void SetAudioVolume(int volume)
        {
            _speechSynthesizer.Volume = volume;
        }
        #endregion

        #region Pronouncation Methods
        public void Speak(string text)
        {
            _speechSynthesizer.Speak(text);
        }
        public async Task SpeakAsync(string text)
        {
            _speechSynthesizer.SpeakAsync(text);
        }
        #endregion

        #region Cancellation Methods
        public void Dispose()
        {
            _speechSynthesizer.Pause();
            _speechSynthesizer.Dispose();
        }
        public void StopAsync()
        {
            _speechSynthesizer.SpeakAsyncCancelAll();
        }
        #endregion
    }
}