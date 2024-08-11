using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Music[] musics;
    private string currentMusicID;
    private void Awake(){
        foreach (Music m in musics){
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
        }
    }

    public void ChangeMusic(string newMusicID){
        foreach (Music m in musics){
            m.source.Stop();
        }
    }
}
