using System;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;

namespace LEARN_WF
{
    public partial class Form1
    {
        // Chặn ghi khi khởi tạo hoặc cập nhật ô nhập.
        private bool updatingInputs;

        // Chặn gửi Execute nhiều lần.
        private bool executingCommand;
        private bool Homed;

        // Homming
        private async void btnHomeAxis_Click(object sender, EventArgs e)
        {

            if (updatingInputs || executingCommand || changingConnection) return;

            if (!PlcMap.Connected || !PlcMap.DataValid) return;

            foreach (JogState state in jogStates)
            {
                if (state.Phase != JogPhase.Idle || state.NeedsReset)
                {
                    AddLog("Hãy kết thúc và xử lý lỗi Jog trước khi Home.");
                    return;

                }
            }
            executingCommand = true;

            try
            {
                bool written = await PlcMap.WriteBoolAsync(plc, PlcMap.HomeAxisAddress, true);

                if (written)
                {
                    AddLog(" Written cmd Home");
                }
                else
                {
                    AddLog((" Write cmd Home Error") + PlcMap.LastWriteError);
                    return;
                }
            }
             catch
            {
                AddLog((" Write cmd Home Error") + PlcMap.LastWriteError);
                return;

            } 

            finally { executingCommand = false; }

        }

        // Move abs

        private async void btnExecuteTGXYZ_Click(object sender, EventArgs e)
        {
            if (updatingInputs || executingCommand || changingConnection) return;

            if (!PlcMap.Connected || !PlcMap.DataValid) return;

            foreach (JogState state in jogStates)
            {
                if (state.Phase != JogPhase.Idle || state.NeedsReset)
                {
                    AddLog("Hãy kết thúc và xử lý lỗi Jog trước khi Move ABS.");
                    return;

                }
            }
            executingCommand = true;

            try
            {
                if (PlcMap.HomedAxis1 || PlcMap.HomedAxis2 || PlcMap.HomedAxis2)
                {
                    bool written = await PlcMap.WriteBoolAsync(plc, PlcMap.ExecuteAbsAddress, true);

                    if (written)
                    {
                        AddLog(" Written cmd Move ABS");
                    }
                    else
                    {
                        AddLog((" Write cmd Move ABS Error") + PlcMap.LastWriteError);
                        return;
                    }
                }
                else AddLog(" Home full axis before move ABS ");
               
            }
            catch
            {
                AddLog((" Write cmd Move ABS Error") + PlcMap.LastWriteError);
                return;

            }

            finally { executingCommand = false; }

        }

        private void btnStopAxis_Click(object sender, EventArgs e)
        {
        }

        private void btnExecuteAbsAxis_Click(object sender, EventArgs e)
        {
        }
    }
}