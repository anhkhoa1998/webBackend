﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Models.Lesson
{
    public class LessonUpdateModel
    {
        public string No { get; set; }
        public string Name { get; set; }
        public string ClassId { get; set; }
    }
}
