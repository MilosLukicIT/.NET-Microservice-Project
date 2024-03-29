﻿using UserMicroservice.Entites;

namespace UserMicroservice.Data
{
    public interface IUserRoleRepository
    {
        List<UserRole> GetUserRoles();
        UserRole GetUserRoleById(Guid userRoleId);
        UserRole CreateUserRole(UserRole userRole);
        UserRole UpdateUserRole(UserRole userRole);
        void DeleteUserRole(Guid userRoleId);
        public bool SaveChanges();
    }
}
