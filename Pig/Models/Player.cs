using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pig.Models
{
    public class Player
    {

        public int score { get; set; }

        public Boolean turn { get; set; }

        public int turnScore = 0;


        public Player(Boolean playerTurn)
        {
            turn = playerTurn;
            score = 0;
        }
    }
}
