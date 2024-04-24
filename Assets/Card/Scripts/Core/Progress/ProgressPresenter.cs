using System;

namespace TheaCard.Core.Progress
{
    public sealed class ProgressPresenter : IProgressPresenter
    {
        private readonly IProgressModel _progressModel;
        private readonly IProgressGUIController _progressGUIController;

        public ProgressPresenter(IProgressModel progressModel, 
            IProgressGUIController progressGUIController)
        {
            _progressModel = progressModel;
            _progressGUIController = progressGUIController;
        }
        
        public void ShowPanel()
        {
            _progressGUIController.HidePanel();
        }

        public void HidePanel()
        {
            _progressGUIController.HidePanel(); 
        }
    }
}