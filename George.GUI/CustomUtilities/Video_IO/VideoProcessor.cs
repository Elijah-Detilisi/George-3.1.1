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

    public class VideoProcessor
    {
        #region Instances
        private readonly CascadeClassifier _faceCascade;
        private Action<Image<Bgr, Byte>> _process;
        #endregion

        public VideoProcessor()
        {
            string cascade_path = @$"{Directory.GetCurrentDirectory()}\Resources\XML Files\haarcascade_frontalface_alt.xml";
            _faceCascade = new CascadeClassifier(cascade_path);
            _process = DummyProcess;
        }

        #region Setter Methods
        public void SetProcess(Action<Image<Bgr, Byte>> process)
        {
            _process = process;
        }

        public void DummyProcess(Image<Bgr, Byte> imageData)
        {
            //Dummy
        }
        #endregion

        #region Image Processing Methods
        public Mat Convert2GrayImage(Image<Bgr, byte> imageFrame)
        {
            Mat grayImage = new Mat();

            CvInvoke.CvtColor(imageFrame, grayImage, ColorConversion.Bgr2Gray);
            CvInvoke.EqualizeHist(grayImage, grayImage);

            return grayImage;
        }
        public void DetectFaces(ref Image<Bgr, Byte> imageFrame)
        {
            var borderColor = new Bgr(Color.SteelBlue);
            Mat grayImage = Convert2GrayImage(imageFrame);

            Image<Bgr, Byte> resultImage = imageFrame.Convert<Bgr, Byte>();
            
            Rectangle[] detectedFaces = _faceCascade.DetectMultiScale(
                image: grayImage,
                scaleFactor: 1.1,
                minNeighbors: 3,
                minSize: Size.Empty,
                maxSize: Size.Empty
            );

            if (detectedFaces.Length > 0)
            {
                foreach (var face in detectedFaces)
                {
                    CvInvoke.Rectangle(
                        img: imageFrame,
                        rect: face,
                        color: borderColor.MCvScalar,
                        thickness: 3
                    );

                    //execute process
                    resultImage.ROI = face;
                    resultImage.Resize(200, 200, Inter.Cubic);
                    _process(resultImage);
                }
            } 
        }
        #endregion
    }
}
