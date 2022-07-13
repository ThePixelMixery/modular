using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

// Log key events to a local file for later analysis.
public class QuickAnalyticsManager : MonoBehaviour
{
    public enum CaptureDetail { LowRateEvent, HighRateEvent };
  
    public bool enableLogging = true;
    public CaptureDetail maxLogRate = CaptureDetail.HighRateEvent;
    public string logFileBaseName = "Ludology";
   
    public string serverName;
    
    private StreamWriter logFile;
    private string DTFormatString = "dd-MM-yyyy-HH-mm-ss.fff";
    
    // Singleton.
    private static QuickAnalyticsManager _S = null;

    private string logDirectory;
    
    void Awake ()
    {
      if (_S == null)
      {
        _S = this;
        logDirectory = Application.persistentDataPath + "/" + "logFiles/";
        Directory.CreateDirectory (logDirectory);
        string logFileName = logDirectory + logFileBaseName + "." + System.DateTime.Now.ToString (DTFormatString) + ".log";
        logFile = new StreamWriter (logFileName, true);
        
        StartCoroutine (uploadFiles ());
      }
    }
    
    public static void logEntry (string id, CaptureDetail level, params System.Object [] parms)
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
    
    public static void logEntry (string id, CaptureDetail level, string message)
    {
      if (_S != null)
      {
        _S.logEntryInternal (id, level, message);
      }
    }
    
    public void logEntryInternal (string id, CaptureDetail level, string message)
    {
      if (enableLogging)
      {
        if (level <= maxLogRate)
        {
          logFile.Write ("\"" + System.DateTime.Now.ToString (DTFormatString) + "\",\"" + id + "\"," + message + "\n");
        }
        if (level == CaptureDetail.LowRateEvent)
        {
          logFile.Flush ();
        }
      }
    }

    public void OnDisable ()
    {
      logFile.Close ();
    }
    
    private static bool TrustCertificate(object sender, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
    {
      // all Certificates are accepted
      return true;
    }
    
    IEnumerator uploadFiles ()
    {
      ServicePointManager.ServerCertificateValidationCallback = TrustCertificate;

      string [] logfiles = Directory.GetFiles (logDirectory);
      
      foreach (string fn in logfiles)
      {
        Debug.Log ("Need to upload: " + fn);

        string data = null;
        try
        {
          data = File.ReadAllText (fn);
        }
        catch (System.Exception e)
        {
          // cannot access file, may happen because file is current active log.
        }
        
        if (data != null)
        {
          string postUri = "https://" + serverName + "/upload";
          using (UnityWebRequest request = UnityWebRequest.Post (postUri, data))
          {
            request.SetRequestHeader ("filename", Path.GetFileName (fn));    
            request.SetRequestHeader ("deviceid", SystemInfo.deviceUniqueIdentifier);    
            request.certificateHandler = new UnityTrustCertificate ();

            yield return request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
//               Debug.Log ("File upload successful");
              File.Delete (fn);
            }
            else
            {
              Debug.Log ("File upload failed: " + fn);
            }
          }
        }
      } 
      yield return null;
    }
}
