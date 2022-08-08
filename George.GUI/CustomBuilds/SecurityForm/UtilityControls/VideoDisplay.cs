using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace George.GUI.CustomBuilds.SecurityForm.UtilityControls
{

    using Emgu.CV;
    using Emgu.CV.Structure;
    using George.GUI.CustomUtilities.Video;
    using System.ComponentModel;

    public partial class VideoDisplay : UserControl
    {
        #region Instances
        private VideoFeed _videoFeed;
        private int _verificationCount;
        private Boolean _shouldDisplayFeed;
        private ImageProcessor _imageProcessor;
        private FaceRecognizer _faceRecognizer;
        private Action<Image<Gray, Byte>>? _currentProcedure;
        #endregion

        public VideoDisplay()
        {
            _videoFeed = new VideoFeed();
            _imageProcessor = new ImageProcessor();
            _faceRecognizer = new FaceRecognizer();
            _verificationCount = 0;
            _shouldDisplayFeed = true;
            _currentProcedure = IdleProcedure;

            InitializeComponent();
            this.Load += VideoDisplay_Load;
        }

        #region Setter Methods
        public void SetProgressText(string text)
        {
            progressLabel.Text = text;
        }
        public void SetProgressTextAsync(string text)
        {
            progressBar.Invoke((MethodInvoker)(() =>
            {
                progressLabel.Text = text;
            }));
        }
        public void SetProgressValue(int value)
        {
            progressBar.Invoke((MethodInvoker)(() =>
            {
                progressBar.Value = value;
                progressBar.Text = $"{value}%";
            }));
        }
        public void SetVideoProcedureStep(int procedureStep)
        {
            if (procedureStep == 1)
            {
                _currentProcedure = TrainingProcedure;
            }
            else if (procedureStep == 2)
            {
                _currentProcedure = PredictProcedure;
            }
            else if (procedureStep == 0)
            {
                _currentProcedure = IdleProcedure;
            }
        }

        public Boolean GetIsTrained()
        {
            return _faceRecognizer.IsTrained;
        }
        #endregion

        #region Procedure Methods
        private void IdleProcedure(Image<Gray, byte>? faceImage)
        {

        }
        private void TrainingProcedure(Image<Gray, byte>? faceImage)
        {
            int count = (int)(_faceRecognizer.GetModelDataCount() * 0.5);

            if (count < 100)
            {
                _faceRecognizer.AppendToModelData(faceImage);
                SetProgressValue(count);
            }
            else
            {
                StopVideoFeed();
                DisplayTraining(1); //Display training

                _faceRecognizer.PrepareModelData();
                _faceRecognizer.TrainModel();  //Start training  

                if (_faceRecognizer.GetModelPerformance() > 90)
                {
                    DisplayTraining(2); //Display training success
                    _currentProcedure = IdleProcedure;
                }
                else
                {
                    DisplayTraining(3); //Display restart training
                    _faceRecognizer.IsTrained = false;
                    _faceRecognizer.ResetModel();
                    ResumeVideoFeed();
                }
            }
        }
        private void PredictProcedure(Image<Gray, byte>? faceImage)
        {
            bool prediction = _faceRecognizer.Predict(faceImage);

            if (prediction && _verificationCount < 100)
            {
                _imageProcessor.ChangeBorderColor(1);
                _verificationCount += 10;
            }
            else if (_verificationCount >= 100)
            {
                StopVideoFeed();
                DisplayLoginSuccess();
                _currentProcedure = IdleProcedure;
            }
            else
            {
                _imageProcessor.ChangeBorderColor(2);
            }

            SetProgressValue(_verificationCount);
        }

        #endregion

        #region Display Methods
        private void DisplayFeed()
        {
            var feed = _videoFeed.GetCurrentImageFrame();
            if (feed != null)
            {
                _imageProcessor.ShowDetectedFaces(feed);
                var faceImage = _imageProcessor.GetFaceROI(feed);
                _currentProcedure(faceImage);

                if (_shouldDisplayFeed)
                {
                    videoPictureBox.BackgroundImage = _imageProcessor.ConvertBgrImageToBitMap(feed);
                }
            }
        }
        private void DisplayTraining(int step)
        {
            if (step == 1)
            {
                SetProgressTextAsync("Training in progress...");
                progressBar.Invoke((MethodInvoker)(() =>
                {
                    progressBar.Hide();
                }));

                videoPictureBox.BackgroundImage = Properties.Resources.machine_learning;
            }
            else if (step == 2)
            {
                _shouldDisplayFeed = false;
                SetProgressTextAsync("Training is complete!");
                videoPictureBox.BackgroundImage = Properties.Resources.finish_line;
            }
            else if (step == 3)
            {
                SetProgressValue(0);
                SetProgressTextAsync("Training failed, retry...");
                progressBar.Invoke((MethodInvoker)(() =>
                {
                    progressBar.Show();
                }));
            }
        }
        public void DisplayDefualtBg()
        {
            videoPictureBox.BackgroundImage = Properties.Resources.face_recognition;
            videoPictureBox.BackgroundImageLayout = ImageLayout.Zoom;
        }
        private void DisplayLoginSuccess()
        {
            _shouldDisplayFeed = false;
            SetProgressTextAsync("Login Success!");
            progressBar.Invoke((MethodInvoker)(() =>
            {
                progressBar.Hide();
            }));

            videoPictureBox.BackgroundImage = Properties.Resources.unlocking;
        }
        public void ClearFeedDisplay()
        {
            _shouldDisplayFeed = true;
            _verificationCount = 0;
            _faceRecognizer.ResetModel();
            _imageProcessor.ChangeBorderColor(1);
            progressBar.Show();
            DisplayDefualtBg();
        }
        #endregion

        #region Video Control Methods
        public void ResumeVideoFeed()
        {
            _videoFeed.OpenCamera();
        }
        public void StopVideoFeed()
        {
            _videoFeed.CloseCamera();
        }
        #endregion

        #region Background Worker Methods
        private void videoBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!videoBgWorker.CancellationPending)
            {
                DisplayFeed();
            }
        }
        private void videoBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            videoBgWorker.RunWorkerAsync();
        }
        #endregion

        #region Event Handler Methods
        private void VideoDisplay_Load(object? sender, EventArgs e)
        {
            InitializeVideoFeed();
            videoBgWorker.RunWorkerAsync();
        }
        private void InitializeVideoFeed()
        {
            _videoFeed.SetDimension(
               width: videoPictureBox.Width,
               hieght: videoPictureBox.Height
            );

            _videoFeed.OpenCamera();
        }
        #endregion

    }
}