using AutoMapper;
using Mongo.API.DTOs;
using Mongo.API.Models;

namespace Mongo.API.MappingProfiles;

public class JokeProfile : Profile
{
    public JokeProfile()
    {
        CreateMap<Joke, JokeDTO>();
        CreateMap<JokeDTO, Joke>();
    }
}