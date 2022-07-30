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
    using George.GUI.CustomUtilities.Video;

    public partial class VideoDisplay : UserControl
    {
        #region Instances
        private VideoFeed _videoFeed;
        #endregion

        public VideoDisplay()
        {
            _videoFeed = new VideoFeed();

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
            progressBar.Value = value;
            progressBar.Text = $"{value}%";
        }
        #endregion

        #region Display Methods
        public void DisplayFeed()
        {
            videoPictureBox.BackgroundImage = _videoFeed.GetCurrentFrameAsBitmap();
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
