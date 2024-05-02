using System.Collections.Generic;
using TheaCard.Core.Enums;

namespace TheaCard.Core.Progress
{
    public interface IProgressModel
    {
        IReadOnlyDictionary<ProgressTypes, int> Progress { get; }
        void AddProgress(ProgressTypes progressType, int amount = 1);
    }
}