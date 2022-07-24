using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace George.GUI.CustomUtilities.Video_IO
{
    using Emgu.CV;
    using Emgu.CV.CvEnum;
    using Emgu.CV.Structure;

    public class VideoFeed
    {
        #region Instances
        private VideoCapture _videoCapture;
        private Image<Bgr, byte> _curretImageFrame;
        private Mat _captureFrame;

        private Control _displayWidget;
        private VideoProcessor _videoProcessor;
        private FaceRecognizer _faceRecognizer;

        #endregion

        public VideoFeed()
        {
            _videoCapture = new VideoCapture();
            _captureFrame = new Mat();
            _videoProcessor = new VideoProcessor();
            _faceRecognizer = new FaceRecognizer();

            _faceRecognizer.SetImageProcessor(_videoProcessor);

        }

        #region Setter Methods
        public void SetDisplayWidget(Control control)
        {
            _displayWidget = control;
        }
        #endregion

        #region Video Handler Methods
        private void _videoCapture_ImageGrabbed(object? sender, EventArgs e)
        {
            _videoCapture.Retrieve(_captureFrame, 0);
            _curretImageFrame = _captureFrame.ToImage<Bgr, Byte>().Resize(
                _displayWidget.Width,
                _displayWidget.Height,
                Inter.Cubic
            );

            _videoProcessor.DetectFaces(ref _curretImageFrame);
            _displayWidget.BackgroundImage = _curretImageFrame.ToBitmap();
        }

        public void OpenCamera()
        {
            if (_videoCapture.IsOpened)
            {
                _videoCapture.ImageGrabbed += _videoCapture_ImageGrabbed;
                _videoCapture.Start();
            }     
        }

        public void CloseCamera()
        {
            Task.Factory.StartNew(() =>
            {
                _videoCapture.Stop();
                _videoCapture.Dispose();
                _videoCapture = new VideoCapture();
            });
        }
        #endregion

    }
}
