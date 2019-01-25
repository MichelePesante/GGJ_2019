using UnityEngine;

public class Timer
{
    private float timer;

    public float GetTimer()
    {
        return timer;
    }

    public void TickTimer()
    {
        timer += Time.deltaTime;
    }

    public void StopTimer()
    {
        timer = 0f;
    }

    public void PauseTimer()
    {
        timer += 0f;
    }

    public bool CheckTimer(float timerToCheck)
    {
        if(timer >= timerToCheck)
        {
            return true;
        }

        return false;
    }
}