using JumbotronGame.Server.DataContracts.Game;
using System;

namespace JumbotronGame.Server.DataContracts.Arena
{
    public class ArenaEvent : Entity
    {
        public string Header { get; set; }

        public Quiz Quiz { get; set; }

        public DateTime Date { get; set; }
    }
}