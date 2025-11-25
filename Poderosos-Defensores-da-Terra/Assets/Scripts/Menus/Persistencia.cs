using UnityEngine;

public class Persistencia
{
    private static ControladorAudio audios;
    private static float _volAmbiente = 0.5f;
    private static float _volMusica = 0.5f;
    private static int _crt = 1;
    
    public static void CarregarPrefs()
    {
        _volAmbiente = PlayerPrefs.GetFloat("Volume Ambiente", 0.5f);
        _volMusica = PlayerPrefs.GetFloat("Volume Música", 0.5f);
        _crt = PlayerPrefs.GetInt("CRT", 1);
    }
    
    public static void SalvarPrefs(float volumeAmbiente, float volumeMusica, int preferenciaCRT)
    {
        PlayerPrefs.SetFloat("Volume Ambiente", volumeAmbiente);
        _volAmbiente = volumeAmbiente;
        
        PlayerPrefs.SetFloat("Volume Música", volumeMusica);
        _volMusica = volumeMusica;
        
        PlayerPrefs.SetInt("CRT", preferenciaCRT);
        _crt = preferenciaCRT;
        
        PlayerPrefs.Save();
    }

    public static float VolumeAmbiente() => _volAmbiente;
    public static float VolumeMusica() => _volMusica;
    public static int CRT() => _crt;
}
