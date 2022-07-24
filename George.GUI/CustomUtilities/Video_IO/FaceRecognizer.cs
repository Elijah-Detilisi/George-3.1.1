using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace George.GUI.CustomUtilities.Video_IO
{
    using Emgu.CV;
    using Emgu.CV.Structure;
    using Emgu.CV.Face;
    using Emgu.CV.CvEnum;

    public class FaceRecognizer
    {

        #region Instances
        private string _modelName;
        private Boolean _isTrained;

        private List<int> _trainingLabel;
        private List<Image<Gray, Byte>> _trainingData;
        private List<Image<Gray, Byte>> _testData;

        private VideoProcessor _imageProcessor;
        private EigenFaceRecognizer _recognizer;
        #endregion

        public FaceRecognizer()
        {
            _isTrained = false;
            _modelName = @$"{Directory.GetCurrentDirectory()}\Resources\XML Files\Recognizer";

            _trainingLabel = new List<int>();
            _trainingData = new List<Image<Gray, byte>>();
            _testData = new List<Image<Gray, byte>>();

            _imageProcessor = new VideoProcessor();
            _recognizer = new EigenFaceRecognizer();

            _imageProcessor.SetProcess(GatherModelData);
        }

        #region Setter Methods
        public void GatherModelData(Image<Bgr, Byte> imageData)
        {
            int sampleSize = 200;
            int faceId = 1;
            var grayImagedata = imageData.Convert<Gray, byte>();

            if (_trainingData.Count() <= sampleSize * 0.75)
            {
                _trainingData.Add(grayImagedata);
            }
            else
            {
                _testData.Add(grayImagedata);
            }

            _trainingLabel.Add(faceId);
        }
        #endregion

        public void TrainModel()
        {
            try
            {
                _recognizer.Train((IInputArrayOfArrays)_trainingData, (IInputArray)_trainingLabel);
                _recognizer.Write(_modelName);
                _isTrained = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Predict(Image<Bgr, Byte> imageData)
        {
            if (_isTrained)
            {
                string result;
                var testImage = imageData.Convert<Gray, byte>();
                var predictionResult = _recognizer.Predict(testImage);

                if (predictionResult.Label > 0)
                {
                    result = "recognized user";
                }
                else
                {
                    result = "unknown";
                }
            }
        }
    }
}
