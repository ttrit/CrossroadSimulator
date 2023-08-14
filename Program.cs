// See https://aka.ms/new-console-template for more information


public partial class Program
{
    public static Queue<Car> northQueue = new Queue<Car>();
    public static Queue<Car> southQueue = new Queue<Car>();
    public static Queue<Car> westQueue = new Queue<Car>();
    public static Queue<Car> eastQueue = new Queue<Car>();

    private static bool _northSouthTurn = true;

    static void Main(string[] args)
    {
        Task.Run(() =>
        {
            while (true)
            {
                northQueue.Enqueue(new Car());
                southQueue.Enqueue(new Car());
                westQueue.Enqueue(new Car());
                eastQueue.Enqueue(new Car());
                Thread.Sleep(60000);
            }
        });

        Task.Run(() =>
        {
            while (true)
            {
                var greenLightObservable = new GreenLightToggleObservable();
                var greenLightObserver = new GreenLightObserver(greenLightObservable);
                greenLightObservable.GreenLight = new GreenLightIndicator
                {
                    IsTurnOn = !_northSouthTurn
                };
                _northSouthTurn = !_northSouthTurn;
                Thread.Sleep(30000);
            }
        });
    }

    public class Car
    {
        public int LeavingTime { get; set; }
    }

    public class GreenLightIndicator
    {
        public bool IsTurnOn { get; set; }
    }

    public class GreenLightObserver
    {
        public GreenLightObserver(GreenLightToggleObservable greenLightObservable)
        {
            greenLightObservable.UpdateGreenLightEvent += GreenLightStatusChanged;
        }

        private void GreenLightStatusChanged(object sender, UpdateGreenLightEventArgs e)
        {
            if (_northSouthTurn)
            {
                northQueue.Dequeue();
                southQueue.Dequeue();
            }
            else
            {
                westQueue.Dequeue();
                eastQueue.Dequeue();
            }
        }
    }

    public class GreenLightToggleObservable
    {
        private GreenLightIndicator greenLight;

        public event EventHandler<UpdateGreenLightEventArgs> UpdateGreenLightEvent = delegate { };

        public GreenLightIndicator GreenLight
        {
            get => greenLight;
            set
            {
                greenLight = value;
                UpdateGreenLightEvent(this, new UpdateGreenLightEventArgs(greenLight));
            }
        }
    }

    public class UpdateGreenLightEventArgs : EventArgs
    {
        public UpdateGreenLightEventArgs(GreenLightIndicator greenLight)
        {
            GreenLightIndicator = greenLight;
        }

        public GreenLightIndicator GreenLightIndicator { get; set; }
    }
}


