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

    public class ImageProcessor
    {
        #region Instances
        private readonly CascadeClassifier _faceCascade;
        #endregion

        public ImageProcessor()
        {
            _faceCascade = new CascadeClassifier(
                Directory.GetCurrentDirectory() + @"\Resources\XML Files\haarcascade_frontalface_alt.xml"
            );
        }

        #region Color Altering Methods
        public Mat? ConvertBgr2Gray(Image<Bgr, byte> imageFrame)
        {
            try
            {
                Mat grayImage = new Mat();
                CvInvoke.CvtColor(imageFrame, grayImage, ColorConversion.Bgr2Gray);
                CvInvoke.EqualizeHist(grayImage, grayImage);
                return grayImage;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[INFO]: Error in ImageProssesor.ConvertBgr2Gray(): " + ex.Message);
                return null;
            }
        }
        #endregion

        #region Face Detection Method
        public Image<Bgr, Byte>? GetDetectFaces(Image<Bgr, Byte> imageFrame)
        {
            try
            {
                Image<Bgr, Byte> resultImage = imageFrame;
                Bgr borderColor = new Bgr(Color.SteelBlue);
                Mat grayImage = ConvertBgr2Gray(imageFrame);

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
                        resultImage.ROI = face;
                    }
                }
                return resultImage;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[INFO]: Error in ImageProssesor.GetDetectFaces(): " + ex.Message);
                return null;
            }
        }
        #endregion
    }
}
