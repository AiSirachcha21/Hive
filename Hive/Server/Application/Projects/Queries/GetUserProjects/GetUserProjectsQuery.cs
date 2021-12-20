﻿using MediatR;
using Hive.Shared.Projects.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Hive.Server.Infrastructure;
using AutoMapper;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Hive.Domain;

namespace Hive.Server.Application.Projects.Queries.GetUserProjects
{
    public record GetUserProjectsQuery(Guid OrganizationId, string UserId) : IRequest<IList<ProjectDisplayViewModel>>;

    public class GetUserProjectsQueryHandler : IRequestHandler<GetUserProjectsQuery, IList<ProjectDisplayViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly MapperConfiguration _mapperConfiguration;

        public GetUserProjectsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
            _mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDisplayViewModel>());
        }

        public async Task<IList<ProjectDisplayViewModel>> Handle(GetUserProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _context.Projects
                .Where(p => p.OrganizationId == request.OrganizationId && p.ProjectUsers.Any(pu => pu.MemberId == request.UserId))
                .ProjectTo<ProjectDisplayViewModel>(_mapperConfiguration)
                .ToListAsync();

            return projects;
        }
    }
}
