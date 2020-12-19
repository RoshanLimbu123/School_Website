using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class DesignationMakerImpl : DesignationMaker
    {
        public void copy(ref Designation Designation, DesignationDto DesignationDto)
        {
            Designation.Designation_id = DesignationDto.Designation_id;
            Designation.name = DesignationDto.name;
            Designation.position = DesignationDto.position;
        }
    }
}
