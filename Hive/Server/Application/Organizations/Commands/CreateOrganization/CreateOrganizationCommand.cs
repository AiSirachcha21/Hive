﻿using AutoMapper;
using Hive.Domain;
using Hive.Server.Infrastructure;
using Hive.Shared.Organizations.CommandViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Organizations.Commands.CreateOrganization
{
    public record CreateOrganizationCommand(string userId, string orgName) : IRequest<CreateOrganizationViewModel>;

    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, CreateOrganizationViewModel>
    {
        private readonly ApplicationDbContext _context;

        public CreateOrganizationCommandHandler(ApplicationDbContext context) => _context = context;

        public async Task<CreateOrganizationViewModel> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            Guid newOrgId = Guid.NewGuid();

            var sysAdmin = await _context.Users.FindAsync(request.userId);
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
                Name = request.orgName,
                Members = members,
                Projects = new List<Project>(),
                SystemAdminId = request.userId,
            };

            await _context.Organizations.AddAsync(org);


            await _context.SaveChangesAsync();
            return new CreateOrganizationViewModel { Id = org.Id, Name = org.Name };

        }
    }
}