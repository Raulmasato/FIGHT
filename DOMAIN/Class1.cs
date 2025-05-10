using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FightingGame.Domain
{
    public class Character
    {
        public int Speed { get; set; }
        public int VariableAttribute { get; set; }
        public int Health { get; private set; }
        private readonly HealthCaretaker _caretaker;
        private int _hitCount;

        public Character(int speed, int variableAttribute)
        {
            Speed = speed;
            VariableAttribute = variableAttribute;
            Health = 100;
            _caretaker = new HealthCaretaker();
            _hitCount = 0;
            SaveState();
        }

        public void ReceiveHit(HitType type)
        {
            int damage;
            switch (type)
            {
                case HitType.Weak:
                    damage = 5;
                    break;
                case HitType.Medium:
                    damage = 10;
                    break;
                case HitType.Strong:
                    damage = 20;
                    break;
                default:
                    damage = 0;
                    break;
            }

            Health = Math.Max(0, Health - damage);
            _hitCount++;
            SaveState();
            if (_hitCount >= 5)
            {
                RestoreLast();
                _hitCount = 0;
            }
        }

        private void SaveState()
        {
            _caretaker.AddMemento(new HealthMemento(Health));
        }

        private void RestoreLast()
        {
            var m = _caretaker.GetPrevious();
            if (m != null) Health = m.State;
        }

        public void RestoreToFull()
        {
            var full = _caretaker.GetMementosUntil(100);
            if (full != null) Health = 100;
        }
    }

    public enum HitType { Weak, Medium, Strong }

    // Memento Pattern
    public class HealthMemento
    {
        public int State { get; private set; }
        public HealthMemento(int health) { State = health; }
    }

    public class HealthCaretaker
    {
        private readonly List<HealthMemento> _mementos;

        public HealthCaretaker()
        {
            _mementos = new List<HealthMemento>();
        }

        public void AddMemento(HealthMemento m)
        {
            _mementos.Add(m);
        }

        public HealthMemento GetPrevious()
        {
            if (_mementos.Count < 2) return null;
            var last = _mementos[_mementos.Count - 2];
            _mementos.RemoveAt(_mementos.Count - 2);
            return last;
        }

        public HealthMemento GetMementosUntil(int target)
        {
            return _mementos.FirstOrDefault(m => m.State == target);
        }
    }
}
