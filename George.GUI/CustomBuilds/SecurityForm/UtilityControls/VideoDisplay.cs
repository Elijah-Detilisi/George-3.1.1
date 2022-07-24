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
    using George.GUI.CustomUtilities.Video_IO;

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

        #region Video Feed Methods
        public void LoadVideoFeed()
        {
            _videoFeed.SetDisplayWidget(videoPictureBox);
            _videoFeed.OpenCamera();
        }
        public void ResumeVideoFeed()
        {
            _videoFeed.OpenCamera();
        }

        public void StopVideoFeed()
        {
            _videoFeed.CloseCamera();
        }
        #endregion

        #region Event Handler Methods
        private void VideoDisplay_Load(object? sender, EventArgs e)
        {
            LoadVideoFeed();
        }
        #endregion
    }
}
