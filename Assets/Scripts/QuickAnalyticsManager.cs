using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Networking;

class GlobalTrustCertificate : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}

// Log key events to a local file for later analysis.
public class QuickAnalyticsManager : MonoBehaviour
{
    public enum CaptureDetail
    {
        LowRateEvent,
        HighRateEvent
    }

    public bool enableLogging = true;

    public CaptureDetail maxLogRate = CaptureDetail.HighRateEvent;

    public string logFileBaseName = "ludology";

    public string serverName;

    private StreamWriter logFile;

    private static string DTFormatString = "dd-MM-yyyy-HH-mm-ss.fff";

    // Singleton.
    private static QuickAnalyticsManager _S = null;

    private string logDirectory;

    public static string timeStamp()
    {
        return System.DateTime.Now.ToString(DTFormatString);
    }

    private void activate()
    {
        if (_S == null)
        {
            _S = this;
            logDirectory = Application.persistentDataPath + "/" + "logFiles/";
            Directory.CreateDirectory (logDirectory);
            string logFileName =
                logDirectory + logFileBaseName + "." + timeStamp() + ".log";
            logFile = new StreamWriter(logFileName, true);

            uploadAndCleanFiles(logDirectory, serverName, this);
            Application.logMessageReceived += LogCallback;
        }
    }

    private void deactivate()
    {
        if (_S != null)
        {
            Application.logMessageReceived -= LogCallback;
            logFile.Close();
            _S = null;
        }
    }

    /*    void Awake()
    {
        activate();
    }
*/
    public static void logEntry(
        string id,
        CaptureDetail level,
        params System.Object[] parms
    )
    {
        string message = "";
        foreach (object o in parms)
        {
            if (o == null)
            {
                message += "\"" + "null" + "\",";
            }
            else
            {
                message += "\"" + o.ToString() + "\",";
            }
        }
        logEntry (id, level, message);
    }

    public static void logEntry(string id, CaptureDetail level, string message)
    {
        if (_S != null)
        {
            _S.logEntryInternal (id, level, message);
            Debug.Log(id + level + message);
        }
    }

    public void logEntryInternal(string id, CaptureDetail level, string message)
    {
        if (enableLogging)
        {
            if (level <= maxLogRate)
            {
                logFile
                    .Write("\"" +
                    System.DateTime.Now.ToString(DTFormatString) +
                    "\",\"" +
                    id +
                    "\"," +
                    message +
                    "\n");
            }
            if (level == CaptureDetail.LowRateEvent)
            {
                logFile.Flush();
            }
        }
    }

    void OnEnable()
    {
        activate();
    }

    public void OnDisable()
    {
        deactivate();
    }

    private static bool
    TrustCertificate(
        object sender,
        X509Certificate x509Certificate,
        X509Chain x509Chain,
        SslPolicyErrors sslPolicyErrors
    )
    {
        // all Certificates are accepted
        return true;
    }

    public static void uploadAndCleanFiles(
        string directory,
        string serverName,
        MonoBehaviour parent
    )
    {
        string[] files = Directory.GetFiles(directory);
        parent.StartCoroutine(uploadFiles(files, serverName));
    }

    public static void uploadAndCleanList(
        List<string> files,
        string serverName,
        MonoBehaviour parent
    )
    {
        parent.StartCoroutine(uploadFiles(files.ToArray(), serverName));
    }

    static IEnumerator uploadFiles(string[] files, string serverName)
    {
        ServicePointManager.ServerCertificateValidationCallback =
            TrustCertificate;

        foreach (string fn in files)
        {
            Debug.Log("Need to upload: " + fn);

            byte[] data = null;
            try
            {
                data = File.ReadAllBytes(fn);
            }
            catch (System.Exception)
            {
                // cannot access file, may happen because file is current active log.
            }

            // Empty log file - can't upload, and need to clear it.
            if ((data != null) && (data.Length == 0))
            {
                File.Delete (fn);
                data = null;
            }

            if (data != null)
            {
                string postUri = "https://" + serverName + "/htdocs/upload";
                using (
                    UnityWebRequest request =
                        new UnityWebRequest(postUri,
                            UnityWebRequest.kHttpVerbPOST)
                )
                {
                    UploadHandlerRaw MyUploadHandler =
                        new UploadHandlerRaw(data);
                    MyUploadHandler.contentType =
                        "application/x-www-form-urlencoded";
                    request.uploadHandler = MyUploadHandler;
                    request.SetRequestHeader("filename", Path.GetFileName(fn));
                    request
                        .SetRequestHeader("deviceid",
                        SystemInfo.deviceUniqueIdentifier);
                    request.certificateHandler = new GlobalTrustCertificate();

                    yield return request.SendWebRequest();

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        Debug.Log("File upload successful");
                        File.Delete (fn);
                    }
                    else
                    {
                        Debug.Log("File upload failed: " + fn);
                    }
                }
            }
        }
        yield return null;
    }

    void LogCallback(string condition, string stackTrace, LogType type)
    {
        //      Debug.Log ("Log: " + condition + "---" + stackTrace + "+++" + type);
        logEntry("ApplicationLevelLog",
        QuickAnalyticsManager.CaptureDetail.LowRateEvent,
        condition,
        stackTrace,
        type.ToString());
    }
}
