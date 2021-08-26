﻿using FreeCourse.Services.Catalog.Dtos.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Dtos.Course
{
    public class CourseUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Image { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategoryId { get; set; }
    }
}