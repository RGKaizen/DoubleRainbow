using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleRainbow
{
    class AnimationThread : BackgroundWorker
    {
        Boolean isOn = false;
        public delegate void ThreadFunction();
        ThreadFunction _tf = null;
        public AnimationThread(ThreadFunction tf)
        {
            this.WorkerSupportsCancellation = true;
            this.DoWork += new System.ComponentModel.DoWorkEventHandler(this.this_DoWork);
            _tf = tf;
        }

        public bool Start() {
            if (!isOn && !this.IsBusy) 
            { 
                this.RunWorkerAsync(10);
                isOn = true;
            }

            return isOn;
        }

        public bool Stop()
        {
            if (isOn)
            {
                this.CancelAsync();
                isOn = false;
            }

            return !isOn;
        }

        private void this_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly. 
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Start the time-consuming operation.
            while (isOn)
            {
                _tf();
            }

            // If the operation was canceled by the user,  
            // set the DoWorkEventArgs.Cancel property to true. 
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }
    }
}
