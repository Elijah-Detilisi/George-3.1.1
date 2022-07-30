using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace George.GUI.CustomUtilities.Video
{
    using Emgu.CV;
    using Emgu.CV.Util;
    using Emgu.CV.Face;
    using Emgu.CV.Structure;
    using System.Diagnostics;

    public class FaceRecognizer
    {
        #region Instances
        private string _modelName;
        private Boolean _isTrained;

        private List<int> _trainingLabel;
        private List<Mat> _trainingData;
        private List<Image<Gray, Byte>> _testData;

        private EigenFaceRecognizer _recognizer;
        #endregion

        public FaceRecognizer()
        {
            _isTrained = false;
            _modelName = Directory.GetCurrentDirectory() + @"\Resources\XML Files\Recognizer";

            _trainingLabel = new List<int>();
            _trainingData = new List<Mat>();
            _testData = new List<Image<Gray, Byte>>();

            _recognizer = new EigenFaceRecognizer();
        }

        #region Initialization Methods
        private void LoadExistingModel()
        {
            try
            {
                _recognizer.Read(_modelName);
                _isTrained = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[INFO]: Error in FaceRecognizer.LoadExistingModel(): " + ex.Message);
            }
        }
        #endregion

        #region Setter and Getter Methods
        public void SetModelData(List<Image<Gray, Byte>> modelData)
        {
            int sampleSize = modelData.Count;
            
            if (sampleSize > 0)
            {
                int faceId = 1;
                int upperQuartile = (int)(sampleSize * 0.75);

                foreach (Image<Gray, Byte> faceData in modelData)
                {
                    if (_trainingData.Count< upperQuartile)
                    {
                        _trainingData.Add(faceData.Mat);
                    }
                    else
                    {
                        _testData.Add(faceData);
                    }
                    _trainingLabel.Add(faceId);
                }
            }
            else
            {
                throw new Exception("[INFO]: Insufficient model data!");
            }
        }
        public bool IsTrained
        {
            get { return _isTrained; }
        }
        #endregion

        #region Recognition Methods
        public void TrainModel()
        {
            if (_trainingData.Count > 0)
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
                    Debug.WriteLine("[INFO]: Error in FaceRecognizer.TrainModel(): " + ex.Message);
                    Console.Beep();
                    Console.Beep();
                }
            }
            else
            {
                throw new Exception("[INFO] : PLEASE SET MODEL DATA BEFORE TRAINING.");
            }
        }

        public Boolean Predict(Image<Gray, Byte> imageData)
        {
            Boolean result = false;

            if (_isTrained)
            {
                var predictionResult = _recognizer.Predict(imageData);
                Debug.WriteLine($"test = {predictionResult.Label} : {predictionResult.Distance}");
                if (predictionResult.Label != -1 && predictionResult.Distance < 2000)
                {
                    result = true;
                }
            }

            return result;
        }
        #endregion

        #region Performance Methods
        public void TestModelPerformance()
        {
            int testPositves = 0;
            int testCount = _testData.Count();

            foreach (var data in _testData)
            {
                bool prediction = Predict(data);
                if (prediction)
                {
                    testPositves++;
                }
            }
            Debug.WriteLine($"[INFO]: Model Testing Results: {testPositves}/{testCount}");
        }
        #endregion
    }
}
