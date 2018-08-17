using System;
using System.Web;
using System.Web.Http;
using AutoMapper;
using System.Collections.Generic;

using FootballData.Domain;
using FootballData.ExternalServices.FootballDataApi.Entities;
using Player = FootballData.ExternalServices.FootballDataApi.Entities.Player;
using Team = FootballData.ExternalServices.FootballDataApi.Entities.Team;

namespace FootballData.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            InitMapper();
        }

        private void InitMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Competition, League>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.FootballDataLeagueId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.League))
                    .ForMember(dest => dest.Teams, opt => opt.MapFrom(src => new List<Domain.Team>()));

                cfg.CreateMap<Team, Domain.Team>();

                cfg.CreateMap<Player, Domain.Player>()
                    .ForMember(dest => dest.ContractUntil, opt => opt.Condition(src => src.ContractUntil != DateTime.MinValue));
            });
        }
    }
}
