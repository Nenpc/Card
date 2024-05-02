using System.Collections.Generic;
using TheaCard.Core.Enums;

namespace TheaCard.Core.Progress
{
    public interface IProgressGUIController
    {
        void UpdateProgress(IReadOnlyDictionary<ProgressTypes, int> progress);
        void HidePopup();
        void ShowPanel();
        void HidePanel();
    }
}