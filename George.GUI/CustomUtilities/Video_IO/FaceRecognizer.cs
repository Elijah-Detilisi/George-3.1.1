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
    using System.Diagnostics;
    using Emgu.CV.Util;

    public class FaceRecognizer
    {

        #region Instances
        private string _modelName;
        private Boolean _isTrained;

        private List<int> _trainingLabel;
        private List<Mat> _trainingData;
        private List<Mat> _testData;

        private VideoProcessor _imageProcessor;
        private EigenFaceRecognizer _recognizer;
        #endregion

        public FaceRecognizer()
        {
            _isTrained = false;
            _modelName = @$"{Directory.GetCurrentDirectory()}\Resources\XML Files\Recognizer";

            _trainingLabel = new List<int>();
            _trainingData = new List<Mat>();
            _testData = new List<Mat>();

            _recognizer = new EigenFaceRecognizer();
        }

        #region Setter Methods
        public void SetImageProcessor(VideoProcessor processor)
        {
            _imageProcessor = processor;
            _imageProcessor.SetProcess(GatherModelData);
        }
        public void GatherModelData(Image<Bgr, Byte> imageData)
        {
            int faceId = 1;
            int sampleSize = 200;

            if ((_trainingData.Count()<=sampleSize * 0.75) && (_testData.Count() <= sampleSize * 0.25))
            {
                var grayImagedata = imageData.Convert<Gray, byte>()
                    .Resize(200, 200, Emgu.CV.CvEnum.Inter.Cubic)
                    .Mat;

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
            else
            {
                Console.Beep();
                TrainModel();
                _imageProcessor.SetProcess(_imageProcessor.DummyProcess);
            }
        }
        public void LoadModel()
        {
            try
            {
                _recognizer.Read(_modelName);
                _isTrained = true;
            }
            catch (Exception)
            {
                _isTrained = false;
            }
        }
        #endregion

        #region Model Methods
        public void TrainModel()
        {
            try
            {
                Debug.WriteLine("[INFO]: Model training...");
                _recognizer.Train(
                    new VectorOfMat(_trainingData.ToArray()),
                    new VectorOfInt(_trainingLabel.ToArray())
                );

                _recognizer.Write(_modelName);
                _isTrained = true;
                Debug.WriteLine("[INFO]: Training complete!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[INFO]: ERROR WHILE TRAINING; {ex.Message}");
                Console.Beep();
                Console.Beep();
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
        #endregion
    }
}
