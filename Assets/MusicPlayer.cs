using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> songs;      // Lista de canciones a reproducir
    private AudioSource audioSource;    // Componente AudioSource para reproducir música
    private int currentSongIndex = 0;  // Índice de la canción actual

    void Start()
    {
        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();

        // Reproducir la primera canción al inicio
        if (songs.Count > 0)
        {
            audioSource.clip = songs[currentSongIndex];
            audioSource.Play();
        }
    }

    // Método para cambiar a la siguiente canción
    public void PlayNextSong()
    {
        // Incrementar el índice de la canción actual
        currentSongIndex++;

        // Si el índice es mayor que la cantidad de canciones, reiniciar
        if (currentSongIndex >= songs.Count)
        {
            currentSongIndex = 0; // Reiniciar a la primera canción
        }

        // Cambiar el clip de audio y reproducirlo
        audioSource.clip = songs[currentSongIndex];
        audioSource.Play();

        Debug.Log("Playing: " + songs[currentSongIndex].name);
    }
}
