﻿namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //***** Navigation Properties  ::: One to One RelationShip between Walk and Navigation property models *****\\

        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }  
    }
}
