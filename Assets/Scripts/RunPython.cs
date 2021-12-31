using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

public class RunPython : MonoBehaviour
{

    //[TextArea]public string cookie = "_uuid=C73E1749-39DC-69B1-99410-46F377EAEEC427326infoc; buvid3=899888D6-77F0-43FA-90E9-CA325E2B8BC1148819infoc; b_nut=1640869327; fingerprint=4262f84bc57bfc89374ea6a4b2336f47; buvid_fp_plain=24C85D82-0B4A-4347-9EB6-6B56A2A8FCC9148808infoc; buvid_fp=899888D6-77F0-43FA-90E9-CA325E2B8BC1148819infoc; b_ut=5; i-wanna-go-back=2; SESSDATA=0da750da%2C1656421926%2C511e7%2Ac1; bili_jct=0da82301fc42c1582626846558cb6920; DedeUserID=394736064; DedeUserID__ckMd5=79866fe76dc55a94; sid=ixxhwvdn; CURRENT_FNVAL=2000; blackside_state=1; rpdid=|(u)~m~JY))l0J'uYR|)~|kkk; b_lsid=B4D24FEE_17E0BD2D3D3; innersign=1; bsource=search_baidu; bp_video_offset_394736064=610031964746587658; PVID=1";

    public static void RunPythonScript(string pyScriptPath,params string[] args)
    {
        Process p = new Process();
        p.StartInfo.FileName = Application.streamingAssetsPath + @"/pachong/venv/Scripts/python.exe";

        if (args!=null)
        {
            foreach (string item in args)
            {
                pyScriptPath += " " + item;
            }
        }

        p.StartInfo.UseShellExecute = false;
        p.StartInfo.Arguments = pyScriptPath;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.CreateNoWindow = true;

        //¿ªÊ¼Ö´ÐÐ
        p.Start();
        p.BeginOutputReadLine();
        p.OutputDataReceived += new DataReceivedEventHandler(Out_RecvData);
       
        p.WaitForExit();
        
    }

    static void Out_RecvData(object sender, DataReceivedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            print(e.Data);
        }
    }

}
