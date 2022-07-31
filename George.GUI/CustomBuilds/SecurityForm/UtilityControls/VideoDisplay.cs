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
    using System.Diagnostics;
    using George.GUI.CustomUtilities.Video;
    
    public partial class VideoDisplay : UserControl
    {
        #region Instances
        private VideoFeed _videoFeed;
        private ImageProcessor _imageProcessor;
        private FaceRecognizer _faceRecognizer;
        private string _proccess;
        #endregion

        public VideoDisplay()
        {
            _proccess = "Training";
            _videoFeed = new VideoFeed();
            _imageProcessor = new ImageProcessor();
            _faceRecognizer = new FaceRecognizer();

            InitializeComponent();
            this.Load += VideoDisplay_Load; 
        }

        #region Setter Methods
        public void SetProgressText(string text)
        {
            progressLabel.Text = text;
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

                if (_proccess == "Training")
                {
                    int count = (int)(_faceRecognizer.GetModelDataCount() * 0.5);
                    _faceRecognizer.AppendToModelData(faceImage);
                    SetProgressValue(count);
                    Debug.WriteLine(count);

                    if (count==100)
                    {
                        _proccess = "Null";
                        _faceRecognizer.PrepareModelData();
                        _faceRecognizer.TrainModel();
                    }
                }
                else if (_proccess == "Predicting")
                {
                    _faceRecognizer.Predict(faceImage);
                }

                videoPictureBox.BackgroundImage = _imageProcessor.ConvertBgrImageToBitMap(feed);
            }
        }
        public void DisplayDefualtBg()
        {
            videoPictureBox.BackgroundImage = Properties.Resources.face_recognition;
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
            DisplayDefualtBg();
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
