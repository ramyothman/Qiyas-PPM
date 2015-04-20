
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
    public partial class ExamSpecialityLogic
    {

        public Qiyas.BusinessLogicLayer.Entity.PPM.ExamSpeciality GetByName(string name)
        {
            Qiyas.BusinessLogicLayer.Entity.PPM.ExamSpeciality examType = null;
            var ex = db.ExamSpecialities.Where(c => c.Name == name).FirstOrDefault();
            if (ex != null)
                examType = new Entity.PPM.ExamSpeciality(ex);
            return examType;
        }

        public bool HasDependencies(int ExaminationTypeID)
        {
            return false;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ExamSpeciality> GetAllActive()
        {
            return db.ExamSpecialities.Where(c => c.IsActive == true).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ExamSpeciality(c) { context = db }).ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ExamSpeciality> GetAllByExamTypeID(int typeID)
        {
            return db.ExamSpecialities.Where(c => c.ExamTypeID == typeID).OrderBy(c => c.Name).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ExamSpeciality(c) { context = db }).ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ExamSpeciality> GetAllActiveByExamTypeID(int typeID)
        {
            return db.ExamSpecialities.Where(c => c.ExamTypeID == typeID && c.IsActive == true).OrderBy(c => c.Name).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ExamSpeciality(c) { context = db }).ToList();
        }

    }
}
      