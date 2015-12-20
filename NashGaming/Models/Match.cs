﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NashGaming.Models
{
    public class Match
    {
        [Key]
        public int MatchID { get; set; }
        [Required]
        public virtual Team Team1 { get; set; }
        [Required]
        public virtual Team Team2 { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Result { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
    }
}