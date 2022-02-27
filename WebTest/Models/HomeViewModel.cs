﻿using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTest.Models
{
    public class HomeViewModel
    {
        public List<Problem> Problems { get; set; }
        public List<ProblemSet> ProblemSets { get; set; }
    }
}
