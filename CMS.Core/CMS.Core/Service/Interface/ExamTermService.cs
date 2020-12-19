using CMS.Core.Dto;
using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
   public interface ExamTermService
    {
        void delete(long exam_term_id);
        void update(ExamtermDto examtermDto);
        void save(ExamtermDto examtermDto);
    }
}
