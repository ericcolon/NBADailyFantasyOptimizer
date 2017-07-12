using NBADailyFantasyOptimizer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBADailyFantasyOptimizer.DataTransfer
{
    public class ActualResultsDto
    {
        public int Day { get; set; }

        public string Name { get; set; }
        public string Position { get; set; }
        public string Team { get; set; }
        public bool IsHome { get; set; }
        public bool IsBench { get; set; }

        public int Minutes { get; set; }
        public int FieldGoalsMade { get; set; }
        public int FieldGoalsAttempted { get; set; }
        public int ThreePointersMade { get; set; }
        public int ThreePointersAttempted { get; set; }
        public int FreethrowsMade { get; set; }
        public int FreethrowsAttempted { get; set; }
        public int OffensiveRebounds { get; set; }
        public int DefensiveRebounds { get; set; }
        public int Assists { get; set; }
        public int Steals { get; set; }
        public int Blocks { get; set; }
        public int Turnovers { get; set; }
        public int PersonalFouls { get; set; }
        public int PlusMinus { get; set; }
        public int Points { get; set; }

        public double Projection { get; set; }

        public double ActualPoints { get; set; }

        public ActualResultsDto() { }

        public ActualResultsDto(PlayerDto actuals)
        {
            if (actuals == null)
                return;

            Day = actuals.Day;
            Minutes = actuals.Minutes;
            FieldGoalsMade = actuals.FieldGoalsMade;
            FieldGoalsAttempted = actuals.FieldGoalsAttempted;
            ThreePointersMade = actuals.ThreePointersMade;
            ThreePointersAttempted = actuals.ThreePointersAttempted;
            FreethrowsMade = actuals.FreethrowsMade;
            FreethrowsAttempted = actuals.FreethrowsAttempted;
            OffensiveRebounds = actuals.OffensiveRebounds;
            DefensiveRebounds = actuals.DefensiveRebounds;
            Assists = actuals.Assists;
            Steals = actuals.Steals;
            Blocks = actuals.Blocks;
            Turnovers = actuals.Turnovers;
            PersonalFouls = actuals.PersonalFouls;
            PlusMinus = actuals.PlusMinus;
            Points = actuals.Points;

            ComputeActuals();
        }

        public void ComputeActuals()
        {
            ActualPoints = Math.Round(ThreePointersMade * ConstantDto.ThreePointPoints
               + Points * ConstantDto.PointPoints
               + (DefensiveRebounds + OffensiveRebounds) * ConstantDto.ReboundPoints
               + Assists * ConstantDto.AssistPoints
               + Steals * ConstantDto.StealPoints
               + Blocks * ConstantDto.BlockPoints
               + Turnovers * ConstantDto.TurnoverPoints, 2);
        }
    }
}
