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

            }
            else
            {

            }
        }

        #endregion


    }
}
