using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hive.Domain;
using Hive.Server.Infrastructure;
using Hive.Shared;
using Hive.Shared.Projects.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Projects.Queries.GetProjectUsersByProjectId
{
    public record GetUsersByProjectIdQuery(Guid ProjectId) : IRequest<List<ProjectUserViewModel>>;

    public class GetUsersByProjectIdQueryHandler : IRequestHandler<GetUsersByProjectIdQuery, List<ProjectUserViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly MapperConfiguration _mapperConfiguration;

        public GetUsersByProjectIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
            _mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, ProjectUserViewModel>()
            .ForMember(dto => dto.Id, target => target.MapFrom(t => t.Id))
            .ForMember(dto => dto.UserName, target => target.MapFrom(t => $"{t.FirstName} {t.LastName}"))
            .ForMember(dto => dto.Role, target => target.Ignore()));
        }

        public async Task<List<ProjectUserViewModel>> Handle(GetUsersByProjectIdQuery request, CancellationToken cancellationToken)
        {
            List<string> projectUsers = await _context.ProjectUsers
                .Where(pu => pu.ProjectId == request.ProjectId)
                .Select(pu => pu.MemberId)
                .ToListAsync(cancellationToken: cancellationToken);

            var users = await _context.Users
                .Where(u => projectUsers.Any(puId => u.Id == puId))
                .ProjectTo<ProjectUserViewModel>(_mapperConfiguration)
                .ToListAsync(cancellationToken: cancellationToken);

            var conversionTasks = new List<Task<ProjectUserViewModel>>();
            var convertedUsers = new List<ProjectUserViewModel>();

            foreach (var user in users)
            {
                conversionTasks.Add(AddRolesToViewModelsAsync(user));
            }

            foreach (var user in await Task.WhenAll(conversionTasks))
            {
                convertedUsers.Add(user);
            }

            return convertedUsers;
        }

        private Task<ProjectUserViewModel> AddRolesToViewModelsAsync(ProjectUserViewModel projectUser)
        {
            string roleId = _context.UserRoles.SingleOrDefault(ur => ur.UserId == projectUser.Id).RoleId;
            string role = _context.Roles.SingleOrDefault(r => r.Id == roleId).Name;
            string fullRoleName = GetFullRoleName(role);

            projectUser.Role = fullRoleName;

            return Task.FromResult(projectUser);
        }

        private static string GetFullRoleName(string role) => role switch
        {
            UserRoles.SystemAdmin => UserRoles.SystemAdminFull,
            UserRoles.ProjectOwner => UserRoles.ProjectOwnerFull,
            UserRoles.Contributer => UserRoles.ContributerFull,
            _ => UserRoles.ContributerFull
        };
    }
}
