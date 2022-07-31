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
        private Rectangle[] _detectedFaces;
        private readonly CascadeClassifier _faceCascade;
        
        #endregion

        public ImageProcessor()
        {
            _detectedFaces = new Rectangle[3];
            _faceCascade = new CascadeClassifier(
                Directory.GetCurrentDirectory() + @"\Resources\XML Files\haarcascade_frontalface_alt.xml"
            );
        }

        #region Setter and Getter Methods
        public Image<Gray, byte>? GetFaceROI(Image<Bgr, Byte> imageFrame)
        {
            if (_detectedFaces != null && _detectedFaces.Length > 0)
            {
                Image<Gray, Byte> resultImage = imageFrame.Convert<Gray, Byte>();
                foreach (var face in _detectedFaces)
                {
                    resultImage.ROI = face;
                }
                return resultImage;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Graphics Altering Methods
        public Mat? ConvertBgr2Gray(Image<Bgr, byte>? imageFrame)
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
        public Bitmap ConvertBgrImageToBitMap(Image<Bgr, byte>? imageFrame)
        {
            return imageFrame.ToBitmap();
        }
        #endregion

        #region Face Detection Methods
        private void DetectFacesFromImage(Image<Bgr, Byte> imageFrame)
        {
            try
            {
                Mat grayImage = ConvertBgr2Gray(imageFrame);
                _detectedFaces = _faceCascade.DetectMultiScale(
                    image: grayImage,
                    scaleFactor: 1.1,
                    minNeighbors: 3,
                    minSize: Size.Empty,
                    maxSize: Size.Empty
                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[INFO]: Error in ImageProssesor.DetectFacesFromImage(): " + ex.Message);
            }
        }

        public void ShowDetectedFaces(Image<Bgr, Byte> imageFrame)
        {
            DetectFacesFromImage(imageFrame);

            if (_detectedFaces != null && _detectedFaces.Length > 0)
            {
                Bgr borderColor = new Bgr(Color.SteelBlue);
                foreach (var face in _detectedFaces)
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
        #endregion

    }
}
