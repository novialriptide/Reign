namespace Faraway.Engine
{
    /// <summary>
    /// Uses `deltaTime` to see how much time has passed. This is used to prevent the
    /// game from proceeding with the times if there was a huge lagspike.
    /// </summary>
    public sealed class Timer
    {
        public bool IsRunning = false;
        public double Time = 0f;

        public Timer() { }

        public Timer(bool runOnStart)
        {
            IsRunning = runOnStart;
        }

        public void Reset()
        {
            Time = 0;
        }
        public void Update(float deltaTime)
        {
            if (IsRunning)
                Time += deltaTime;
        }
    }
}
