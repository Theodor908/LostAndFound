using System;

namespace LostAndFound.Models;

public class UserRoleAssignmentDTO
{
    public int UserId { get; set; }
    public List<RoleAssignmentDTO> RoleAssignments { get; set; } = new List<RoleAssignmentDTO>();
}
