using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class OutletMakerImpl : OutletMaker
    {
        public void copy( Outlet outlet , OutletDto outletDto )
        {
            outlet.outlet_id = outletDto.outlet_id;
            outlet.title = outletDto.title.Trim();
            outlet.address = outletDto.address.Trim();
            outlet.description = outletDto.description.Trim();
            if (!string.IsNullOrWhiteSpace(outletDto.file_name))
            {
                outlet.file_name = outletDto.file_name;
            }
           
            outlet.is_enabled = outletDto.is_enabled;
        }
    }
}
