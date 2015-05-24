
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class ExamCenterRequiredExam
    {
        [Display(Name = "ExamPeriodName")]
        public string ExamPeriodName
        {
            get
            {
                var item = context.ExamPeriods.Where(c => c.ExamPeriodID == ExamPeriodID).FirstOrDefault();
                if (item != null)
                    return item.Name;
                else
                    return "";
            }
        }

        [Display(Name = "CenterCode")]
        public string ExamCenterCode
        {
            get
            {
                var item = context.ExamCenters.Where(c => c.ExaminationCenterID == ExamCenterID).FirstOrDefault();
                if (item != null)
                    return item.CenterCode;
                else
                    return "";
            }
        }

        [Display(Name = "ExamCenterName")]
        public string ExamCenterName
        {
            get
            {
                var item = context.ExamCenters.Where(c => c.ExaminationCenterID == ExamCenterID).FirstOrDefault();
                if (item != null)
                    return item.Name;
                else
                    return "";
            }
        }

        private string _PackageCode;
        [Display(Name = "PackageCode")]
        public string PackageCode
        {
            set { _PackageCode = value; }
            get { return _PackageCode; }
        }
    }
}
      