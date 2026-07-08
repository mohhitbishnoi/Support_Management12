using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.TicketDtos;

public class GetAllTicketDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public int CustomerId { get; set; }

    public Guid? AssignedEmployeeId { get; set; }

    public DateTime CreateDate { get; set; }
}
