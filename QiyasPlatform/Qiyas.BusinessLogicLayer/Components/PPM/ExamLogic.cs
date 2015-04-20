
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.ComponentModel;

namespace Qiyas.BusinessLogicLayer.Components.PPM
{
    public partial class ExamLogic
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.Exam> GetAllView()
        {
            List<Qiyas.BusinessLogicLayer.Entity.PPM.Exam> exams = new List<Entity.PPM.Exam>();
            var examsView = db.ExamViews.ToList();
            foreach(var item in examsView)
            {
                Qiyas.BusinessLogicLayer.Entity.PPM.Exam exam = new Entity.PPM.Exam();
                exam.CreatedDate = item.CreatedDate;
                exam.CreatorID = item.CreatorID;
                exam.ExamCode = item.ExamCode;
                exam.ExamID = item.ExamID;
                exam.ExamSpecialityID = item.ExamSpecialityID;
                exam.ExamTypeID = item.ExamTypeID;
                exam.IsActive = item.IsActive;
                exam.ModifiedByID = item.ModifiedByID;
                exam.ModifiedDate = item.ModifiedDate;
                exam.Name = item.Name;
                exam.Notes = item.Notes;
                exam.NumberofPages = item.NumberofPages;
                exam.NumberofSections = item.NumberofSections;
                exam.StudentGenderID = item.StudentGenderID;
                exam.TimeForSection = item.TimeForSection;
                exams.Add(exam);
            }
            return exams;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.Exam> GetAllActive()
        {
            return db.Exams.Where(c => c.IsActive == true).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.Exam(c) { context = db }).ToList();
        }

        public Qiyas.BusinessLogicLayer.Entity.PPM.Exam GetByName(string name)
        {
            Qiyas.BusinessLogicLayer.Entity.PPM.Exam examType = null;
            var ex = db.Exams.Where(c => c.Name == name).FirstOrDefault();
            if (ex != null)
                examType = new Entity.PPM.Exam(ex);
            return examType;
        }
        public bool HasDependencies(int ExaminationTypeID)
        {
            return false;
        }

        public int GetExamModelCount(int ExamID)
        {
            return db.GetExamModelCount(ExamID).Value;
        }
    }
}
      