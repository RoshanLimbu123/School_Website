using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class GalleryMakerImpl : GalleryMaker
    {
      
        public void copy(Gallery gallery1, GalleryDto gallery1Dto)
        {
            gallery1.description = gallery1Dto.description;
            gallery1.gallery_id = gallery1Dto.gallery_id;
            gallery1.is_active = gallery1Dto.is_active;
            gallery1.name = gallery1Dto.name;


        }
    }
}
