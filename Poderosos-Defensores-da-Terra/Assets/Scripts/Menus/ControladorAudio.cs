using System.Collections.Generic;
using UnityEngine;

public class ControladorAudio : MonoBehaviour
{
    [SerializeField]
    public List<AudioSource> sonsAmbiente;
    public AudioSource musica;

    private void Start() {
        Atualizar(Persistencia.VolumeAmbiente(), Persistencia.VolumeMusica());
    }

    public void Atualizar(float volumeAmbiente, float volumeMusica)
    {
        foreach (var audio in sonsAmbiente)
            audio.volume = volumeAmbiente;

        musica.volume = volumeMusica;
    }
}
