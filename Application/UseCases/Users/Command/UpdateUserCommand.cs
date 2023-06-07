using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.IdentityEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Command;

public class UpdateUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string? UserName { get; init; }
    public string? Password { get; init; }
    public Guid[]? RoleIds { get; init; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashPassword _hashPassword;

    public UpdateUserCommandHandler(IApplicationDbContext context, IHashPassword hashPassword)
           => (_context, _hashPassword) = (context, hashPassword);

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
            throw new NotFoundException(nameof(User), request.Id);


        //command property if null, not sets value
        var properties = typeof(UpdateUserCommand).GetProperties();
        foreach (var property in properties)
        {

            var requestValue = property.GetValue(request);
            if (requestValue is not null && property.Name is not "RoleIds")
            {
                var entityProperty = entity.GetType().GetProperty(property.Name);
                entityProperty.SetValue(entity, requestValue);

            }

            if (property.Name is "Password" && requestValue is not null)
            {
                entity.Password = await _hashPassword.GetHashPasswordAsync(request.Password);
            }

        }

        entity.LastUpdated = DateTime.Now;
        entity.LastUpdatedBy = request.UserName;
        if (request.RoleIds is not null)
        {
            List<Role> foundRoles = new();

            foreach (var roleId in request.RoleIds)
            {
                var role = await _context.Roles.FindAsync(new object[] { roleId });
                foundRoles.Add(role);
            }
            entity.Roles = foundRoles;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
