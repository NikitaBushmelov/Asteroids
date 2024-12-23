using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public ParticleSystem explosion;
    public int lives;
    public int score;
    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityFrames = 3.0f;

    public void AsteroidDestroyed(AsteroidController asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();
        if(asteroid.size < 0.8f)
        {
            score += 100;
        }else if (asteroid.size < 1.2f)
        {
            score += 50;
        }else {
            score += 25;    
        }
    }
    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        this.lives--;
        if (this.lives <= 0) {
            GameOver();
        }
        else{
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), respawnInvulnerabilityFrames);
    }
    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    void GameOver()
    {


    }
}
