public class Timer
{
    public float timer;
    float startTime;

    public Timer()
    {
        timer = 0;
    }

    public Timer(float time)
    {
        timer = time;
        startTime = time;
    }

    public void countUp(float seconds)
    {
        timer += seconds;
    }

    public void countDown(float seconds)
    {
        if (timer > 0) timer -= seconds;
    }

    public void reset()
    {
        timer = startTime;
    }

    public string toString()
    {
        return ((int)(timer / 60)) + ":" + ((int)(timer % 60));
    }
}