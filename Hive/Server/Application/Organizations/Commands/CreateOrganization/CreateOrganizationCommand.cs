using Hive.Domain;
using Hive.Server.Infrastructure;
using Hive.Shared;
using Hive.Shared.Organizations.CommandViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Organizations.Commands.CreateOrganization
{
    public record CreateOrganizationCommand(string UserId, string OrgName) : IRequest<CreateOrganizationViewModel>;

    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, CreateOrganizationViewModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateOrganizationCommandHandler(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<CreateOrganizationViewModel> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            Guid newOrgId = Guid.NewGuid();

            var sysAdmin = await _context.Users.FindAsync(request.UserId);
            List<OrganizationUser> members = new();

            members.Add(new OrganizationUser
            {
                Id = Guid.NewGuid(),
                MemberId = sysAdmin.Id,
                OrganizationId = newOrgId
            });

            Organization org = new()
            {
                Id = newOrgId,
                Name = request.OrgName,
                Members = members,
                Projects = new List<Project>(),
                SystemAdminId = request.UserId,
            };

            await _userManager.AddToRoleAsync(sysAdmin, UserRoles.SystemAdmin);
            await _context.Organizations.AddAsync(org, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return new CreateOrganizationViewModel { Id = org.Id, Name = org.Name };

        }
    }
}
