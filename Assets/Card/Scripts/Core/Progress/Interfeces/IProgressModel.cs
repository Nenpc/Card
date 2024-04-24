using System.Collections.Generic;

namespace TheaCard.Core.Progress
{
    public interface IProgressModel
    {
        IReadOnlyDictionary<ProgressTypes, int> Progress { get; }
    }
}