using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
   public interface ExamTermRepository
    {
        void insert(ExamTerm exam_Term);
        void update(ExamTerm exam_Term);
        void delete(ExamTerm exam_Term);
        ExamTerm getById(long exam_term_id);

        List<ExamTerm> getAll();
        IQueryable<ExamTerm> getQueryable();
    }
}
