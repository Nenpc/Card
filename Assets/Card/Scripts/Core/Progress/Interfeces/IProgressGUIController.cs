using System.Collections.Generic;

namespace TheaCard.Core.Progress
{
    public interface IProgressGUIController
    {
        void UpdateProgress(IReadOnlyDictionary<ProgressTypes, int> progress);
        void ShowPanel();
        void HidePanel();
    }
}