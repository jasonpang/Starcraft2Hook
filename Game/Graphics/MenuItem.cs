using Game.Extensions;

namespace Game.Graphics
{
    public class MenuItem
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        public MenuItem(string name, int value, int min, int max)
        {
            Name = name;
            Value = value;
            Minimum = min;
            Maximum = max;
        }

        public void IncrementValue(int by)
        {
            Value = (Value + by).Clamp(Minimum, Maximum);
        }

        public void DecrementValue(int by)
        {
            Value = (Value - by).Clamp(Minimum, Maximum);
        }
    }
}
