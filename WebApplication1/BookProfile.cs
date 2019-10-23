﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class BookProfile : Profile
    {
        
        public BookProfile()
        {
            CreateMap<Entities.Book, Models.Book>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom
                   (src => $"{src.Author.FirstName} {src.Author.LastName }"));
             CreateMap<Models.BookForCreation, Entities.Book>();

            CreateMap<Entities.Book, Models.BookWithCovers>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => $"{src.Author.FirstName}{src.Author.LastName}"));

            CreateMap<IEnumerable<ExternalModels.BookCover>, Models.BookWithCovers>()
               .ForMember(dest => dest.BookCovers, opt => opt.MapFrom(src => src));
 
        }
    }
}
