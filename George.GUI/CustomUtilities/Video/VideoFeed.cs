using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace George.GUI.CustomUtilities.Video
{
    using Emgu.CV;
    using Emgu.CV.CvEnum;
    using Emgu.CV.Structure;
    using System.Diagnostics;

    public class VideoFeed
    {

        #region Instances
        private Mat _captureFrame;
        private VideoCapture _videoCapture;
        private Dimensions _imageDimensions;
        private Image<Bgr, byte> _curretImageFrame;
        #endregion

        public VideoFeed()
        {
            _videoCapture = new VideoCapture();
            _imageDimensions = new Dimensions();
            _captureFrame = new Mat();
            
            SetDimension(500, 500);
        }

        #region Setter and Getter Methods
        public Image<Bgr, byte> CurretImageFrame
        {
            set { _curretImageFrame = value; }
            get { return _curretImageFrame; }
        }
        public void SetDimension(int width, int hieght)
        {
            _imageDimensions.width = width;
            _imageDimensions.height = hieght;
        }
        #endregion

        #region Camera Control Methods
        public void OpenCamera()
        {
            Debug.WriteLine("[INFO]: Opening camera...");

            if (_videoCapture.IsOpened)
            {
                _videoCapture.ImageGrabbed += _videoCapture_ImageGrabbed;
                _videoCapture.Start();
            }
        }
        public void CloseCamera()
        {
            Debug.WriteLine("[INFO]: Closing camera...");
            Task.Factory.StartNew(() =>
            {
                _videoCapture.Stop();
                _videoCapture.Dispose();
                _videoCapture = new VideoCapture();
            });
        }
        #endregion

        #region Video Handler Methods
        private void _videoCapture_ImageGrabbed(object? sender, EventArgs e)
        {
            _videoCapture.Retrieve(_captureFrame, 0);
            _curretImageFrame = _captureFrame.ToImage<Bgr, Byte>().Resize(
                _imageDimensions.width,
                _imageDimensions.height,
                Inter.Cubic
            );
        }
        #endregion

        #region Supporting Entities
        private struct Dimensions
        {
            public int width;
            public int height;
        }
        #endregion

    }
}
