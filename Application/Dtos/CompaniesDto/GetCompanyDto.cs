using System;
using System.Collections.Generic;
using System.Text;
using Application.Commons;

namespace Application.Dtos.CompaniesDto;

public class GetCompanyDto:BaseDto
{
    public string Name { get; set; }
    public string logo { get; set; }

}
