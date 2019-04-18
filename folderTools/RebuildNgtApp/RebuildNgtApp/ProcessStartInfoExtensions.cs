using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebuildNgtApp
{
    public static class ProcessStartInfoExtensions
    {
        public static void ApplyNoWindowConfig(this Process process, Action<object, DataReceivedEventArgs> outputDataReceiver)
        {
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WorkingDirectory = @"d:\";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

            DataReceivedEventHandler del = new DataReceivedEventHandler(outputDataReceiver);
            process.OutputDataReceived += del;
        }

        public static void ApplyVisibleWindowConfig(this Process process)
        {
            process.StartInfo.RedirectStandardOutput = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = false;
            process.StartInfo.WorkingDirectory = @"d:\";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        }


    }
}
