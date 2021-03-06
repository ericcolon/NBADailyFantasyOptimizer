﻿using NBADailyFantasyOptimizer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBADailyFantasyOptimizer.DataTransfer
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public string Team { get; set; }
        public string Opponent { get; set; }
        public bool IsHome { get; set; }
        public bool IsBench { get; set; }
        public double Projection { get; set; }
        public double FantasyPointsPerGame { get; set; }
        public string InjuryStatus { get; set; }

        public double ActualPoints { get; set; }
        public bool TopPlayerAtPositionForTeam { get; set; }
        public bool TopPlayerForTeam { get; set; }
        public int Day { get; set; }
        public double Roi { get; set; }

        public double Previous5DayAverage { get; set; }
        public double SeasonAverage { get; set; }
        public double SeasonFloor { get; set; }
        public double SeasonCeiling { get; set; }
        public double ComputedProjection { get; set; }

        public List<ActualResultsDto> ActualResults { get; set; }
        public List<ActualResultsDto> ActualResultsBeforeDay { get; set; }
        public List<ProjectionDto> Projections = new List<ProjectionDto>();
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

        public void AddProjection(ProjectionDto projection)
        {
            if(projection == null)
                return;

            Projection = projection.Projection;
            //Previous5DayAverage = projection.Previous5DayAverage;
            //SeasonCeiling = projection.SeasonCeiling;
            //SeasonAverage = projection.SeasonAverage;
            //SeasonFloor = projection.SeasonFloor;
            //ComputedProjection = projection.ComputedProjection;

            Roi = Projection / Salary;
        }

        public void AddPreviousProjection(List<ProjectionDto> projections)
        {
            Projections.AddRange(projections);
        }

        public void AddActuals(ActualResultsDto actuals, bool isActualDay)
        {
            if (actuals == null)
                return;

            if (ActualResults == null)
            {
                ActualResults = new List<ActualResultsDto>();
                ActualResultsBeforeDay = new List<ActualResultsDto>();
            }

            actuals.ComputeActuals();

            ActualResults.Add(actuals);

            if (isActualDay)
            {
                Minutes = actuals.Minutes;
                ActualPoints = ActualResults.OrderByDescending(s => s.Day).First().ActualPoints;
            }
            else
            {
                ActualResultsBeforeDay.Add(actuals);
            }

            var matchingProj = Projections.Find(p => p.Day == actuals.Day);
            if (matchingProj != null)
                actuals.Projection = matchingProj.Projection;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
