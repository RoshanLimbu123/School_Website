using CMS.Core.Dto;
using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Interface
{
  public  interface ExamTermMaker
    {
        void copy(ExamTerm exam_Term, ExamtermDto examtermDto);
    }
}
