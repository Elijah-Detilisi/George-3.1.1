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
        #endregion

        public VideoProcessor()
        {
            string cascade_path = @$"{Directory.GetCurrentDirectory()}\Resources\XML Files\haarcascade_frontalface_alt.xml";
            _faceCascade = new CascadeClassifier(cascade_path);
        }

        public void DetectFaces(ref Image<Bgr, byte> imageFrame)
        {
            Mat grayImage = new Mat();
            var borderColor = new Bgr(Color.SteelBlue);

            CvInvoke.CvtColor(imageFrame, grayImage, ColorConversion.Bgr2Gray);
            CvInvoke.EqualizeHist(grayImage, grayImage);

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
                }
            }
        }

    }
}
