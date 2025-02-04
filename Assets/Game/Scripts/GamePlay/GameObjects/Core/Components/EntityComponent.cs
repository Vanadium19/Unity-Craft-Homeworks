using System;
using System.Collections.Generic;

namespace Game.Core.Components
{
    public abstract class EntityComponent
    {
        private List<Func<bool>> _conditions;

        public void AddCondition(Func<bool> condition)
        {
            _conditions.Add(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _conditions.Remove(condition);
        }

        public bool CheckConditions()
        {
            foreach (var condition in _conditions)
            {
                if (!condition.Invoke())
                {
                    return false;
                }
            }

            return true;
        }
    }
}