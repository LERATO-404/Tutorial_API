using AutoMapper;
using Tutorial_API.Models;
using Tutorial_API.Dtos;

namespace Tutorial_API.Profiles{

    public class TutorialProfile : Profile
    {
        //Mapping the tutor model to the Dto
        public TutorialProfile(){

            //Source -> Targer
            //create
            CreateMap<Tutorial, TutorialReadDto>();
            CreateMap<TutorialCreateDto, Tutorial>();

            //update -- PUT
            CreateMap<TutorialUpdateDto, Tutorial>();
            
            //update -- PATCH
            CreateMap<Tutorial, TutorialUpdateDto>();
        }
    }
}