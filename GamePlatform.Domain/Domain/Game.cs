﻿using System.Security.Policy;

namespace GamePlatform.Domain.Domain
{
    public class Game : BaseEntity
    {
        public string? GameTitle { get; set; }
        public string? GameDesc { get; set; }
        public DateTime DateReleased { get; set; }
        public string? GameImage { get; set; }
        public int Rating { get; set; }
        public int Price { get; set; }

        public Guid? DeveloperId { get; set; }
        public Developer? Developer { get; set; }
    }
}
