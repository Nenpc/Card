using System;
using System.Collections.Generic;

namespace TheaCard.Core.Progress
{
    public sealed class ProgressModel : IProgressModel
    {
        private Dictionary<ProgressTypes, int> _progress = new Dictionary<ProgressTypes, int>();

        public IReadOnlyDictionary<ProgressTypes, int> Progress => _progress;

        public ProgressModel()
        {
            foreach (ProgressTypes progressType in Enum.GetValues(typeof(ProgressTypes)))
            {
                if ((int)progressType != 0)
                    _progress.Add(progressType, 0);
            }
        }

        public void AddProgress(ProgressTypes progressType, int amount = 1)
        {
            _progress.Add(progressType, amount);
        }
    }
}