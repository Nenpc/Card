using System;
using System.Collections.Generic;
using TheaCard.Core.Enums;
using UnityEngine;

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
            if (_progress.ContainsKey(progressType))
                _progress[progressType] += amount;
            else
                Debug.LogWarning($"Can't find progress info for {progressType}");
        }
    }
}