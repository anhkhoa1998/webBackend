﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Models.Chapter
{
    public class ChapterUpdateModel
    {
        public string Theory { get; set; }
        public string Code { get; set; }
        public int No { get; set; }
        public string LessonId { get; set; }
        public string Name { get; set; }
    }
}
