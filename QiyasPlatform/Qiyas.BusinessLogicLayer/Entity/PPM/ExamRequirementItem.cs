
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class ExamRequirementItem
    {
        public ExamRequirementItem(int ID, int ExamID)
        {
            this.entity = context.ExamRequirementItems.Where(p => p.ExamCenterRequiredExamsID == ID && p.ExamID == ExamID).FirstOrDefault();
        }
    }
}
      