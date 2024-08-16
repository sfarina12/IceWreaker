
using UnityEngine;

public class playerShake : MonoBehaviour
{
    [Tooltip("Time to wait before another camera shake after the monster lost sight of the player")]
    public float lostSight;
    public float shakeIncencity = 0.7f;
    [Space]
    public ai_controller AI;
    public CameraShake shaker;
    public audioPlayer foundYou;

    float timer = 0;

    void Update() {
        //deve fare lo shake della telecamenra quando il mostro lo trova
        //lo shade non deve spammarlo ma solo quando il mostro le perde di vista per tot secondi

        if(AI.isChasing) {            
            if(timer <= 0) { 
                shaker.shakeCamera(shakeIncencity); 
                foundYou.playAudio(); 
            }
            timer = lostSight;
        }
    
        if(timer > 0) { timer -= Time.deltaTime; }
    }
}
