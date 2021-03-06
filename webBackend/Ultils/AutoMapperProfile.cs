﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webBackend.Models.Answer;
using webBackend.Models.Chapter;
using webBackend.Models.Class;
using webBackend.Models.Group;
using webBackend.Models.Issue;
using webBackend.Models.Lesson;
using webBackend.Models.Result;
using webBackend.Models.User;

namespace webBackend.Ultils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserModel, User>();
            CreateMap<UserUpdateModel, User>();
            CreateMap<Groups, GroupResult>();
            CreateMap<ClassModel, Class>();
            CreateMap<ClassUpdateModel, Class>();
            CreateMap<Class, ClassResult>();
            CreateMap<LessonModel, Lesson>();
            CreateMap<LessonUpdateModel, Lesson>();

            CreateMap<ChapterModel, Chapter>();
            CreateMap<ChapterUpdateModel, Chapter>();

            CreateMap<IssueModel, Issue>();
            CreateMap<IssueUpdateModel, Issue>();

            CreateMap<AnswerModel, Answer>();
            CreateMap<AnswerUpdateModel, Answer>();
        }
    }
}
