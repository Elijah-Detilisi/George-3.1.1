using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    using System.Diagnostics;
    using George.GUI.CustomUtilities.Video;
    
    public partial class VideoDisplay : UserControl
    {
        #region Instances
        private Action<Image<Gray, Byte>> _currentProcedure;
        private VideoFeed _videoFeed;
        private ImageProcessor _imageProcessor;
        private FaceRecognizer _faceRecognizer;
        
        #endregion

        public VideoDisplay()
        {
            _currentProcedure = IdleProcedure;
            _videoFeed = new VideoFeed();
            _imageProcessor = new ImageProcessor();
            _faceRecognizer = new FaceRecognizer();

            InitializeComponent();
            this.Load += VideoDisplay_Load; 
        }

        #region Setter Methods
        public void SetProgressText(string text)
        {
            progressBar.Invoke((MethodInvoker)(() => {
                progressLabel.Text = text;
            }));
        }
        public void SetProgressValue(int value)
        {
            progressBar.Invoke((MethodInvoker)(() => {
                progressBar.Value = value;
                progressBar.Text = $"{value}%";
            }));
        }
        #endregion

        #region Display Methods
        public void DisplayFeed()
        {
            var feed = _videoFeed.GetCurrentImageFrame();
            if (feed != null)
            {
                _imageProcessor.ShowDetectedFaces(feed);
                var faceImage = _imageProcessor.GetFaceROI(feed);
                _currentProcedure(faceImage);
                
                videoPictureBox.BackgroundImage = _imageProcessor.ConvertBgrImageToBitMap(feed);
            }
        }
        public void DisplayDefualtBg()
        {
            videoPictureBox.BackgroundImage = Properties.Resources.face_recognition;
            videoPictureBox.BackgroundImageLayout = ImageLayout.Zoom;
        }
        private void DisplayTraining(int step)
        {
            if (step == 1)
            {
                SetProgressText("Training in progress...");
                progressBar.Invoke((MethodInvoker)(() => {
                    progressBar.Hide();
                }));

                videoPictureBox.BackgroundImage = Properties.Resources.machine_learning;
            }
            else if (step == 2)
            {
                SetProgressText("Training is complete!");
                SetProgressValue(0);
                progressBar.Invoke((MethodInvoker)(() => {
                    progressBar.Show();
                }));

                videoPictureBox.Image = Properties.Resources.finish_line;
            }

            videoPictureBox.BackgroundImageLayout = ImageLayout.Zoom;
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

        #region Procedure Methods
        private void IdleProcedure(Image<Gray, byte> faceImage)
        {

        }
        private void TrainingProcedure(Image<Gray, byte> faceImage)
        {
            int count = (int)(_faceRecognizer.GetModelDataCount() * 0.5);

            if (count < 100)
            {
                _faceRecognizer.AppendToModelData(faceImage);
                SetProgressValue(count);
            }
            else if(!_faceRecognizer.IsTrained)
            {
                _faceRecognizer.PrepareModelData();

                StopVideoFeed();
                DisplayTraining(1); //Display training
                _faceRecognizer.TrainModel();  //Start training  
                DisplayTraining(2); //Display Complete
            }
        }

        private void PredictProcedure(Image<Gray, byte> faceImage)
        {

        }

        #endregion

    }
}
