using System;
using System.Collections.Generic;
using System.Linq;
using TheaCard.Core.Enums;
using TheaCard.Core.FightModel;
using TheaCard.Infrastructure.Intermediary;

namespace TheaCard.Core.Process
{
    public sealed class ProcessIntermediary : IntermediaryAbstract<ProcessStates>
    {
        private readonly IEnumerable<IIntermediaryState<ProcessStates>> _states;
        private readonly IFightModel _fightModel;
        
        protected override ProcessStates FirstState => ProcessStates.Player;
        protected override IEnumerable<IIntermediaryState<ProcessStates>> States => _states;

        public ProcessIntermediary(IEnumerable<IIntermediaryState<ProcessStates>> states, IFightModel fightModel)
        {
            _states = states;
            _fightModel = fightModel;
        }
        
        protected override ProcessStates GetNextState(ProcessStates states)
        {
            if (states == ProcessStates.Fight)
                return ProcessStates.None;
            
            var freePlayerHero = _fightModel.Player.HeroesModel.FirstOrDefault(x => !x.OnBoard);
            var freeEnemyHero = _fightModel.Enemy.HeroesModel.FirstOrDefault(x => !x.OnBoard);
            var fightState = freePlayerHero == default && freeEnemyHero == default;
            
            if (fightState)
                return ProcessStates.Fight;

            if (states == ProcessStates.Player)
                return ProcessStates.AI;
            
            if (states == ProcessStates.AI)
                return ProcessStates.Player;

            return ProcessStates.None;
        }
    }
}