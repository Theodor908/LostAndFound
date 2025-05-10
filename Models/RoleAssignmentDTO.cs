using System;
using LostAndFound.Interfaces;

namespace LostAndFound.Models;

public class RoleAssignmentDTO
{
    public RoleDTO Role { get; set; } = new RoleDTO();
    public bool IsAssigned { get; set; }

}
