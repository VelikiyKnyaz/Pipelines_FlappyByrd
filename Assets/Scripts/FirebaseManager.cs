using UnityEngine;
using System.Runtime.InteropServices;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    #if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")] private static extern void InitFirebaseBridge();
    [DllImport("__Internal")] private static extern void SendGameStartTelemetry();
    [DllImport("__Internal")] private static extern void SendGameEndTelemetry(int score, int pipesPassed);
    #else
    private static void InitFirebaseBridge() { }
    private static void SendGameStartTelemetry() => Debug.Log("Telemetry: Game Start");
    private static void SendGameEndTelemetry(int score, int pipesPassed) => Debug.Log($"Telemetry: Game End. Score: {score}, Pipes: {pipesPassed}");
    #endif

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        InitFirebaseBridge();
    }

    public void TrackGameStart()
    {
        SendGameStartTelemetry();
    }

    public void TrackGameEnd(int finalScore, int totalPipesPassed)
    {
        SendGameEndTelemetry(finalScore, totalPipesPassed);
    }

    public void SubmitScore(int score, int pipes, int duration)
    {
        // Redirige la llamada antigua al nuevo flujo para no romper nada
        TrackGameEnd(score, pipes);
    }

    public void OnAuthReceived(string json) { }
}
