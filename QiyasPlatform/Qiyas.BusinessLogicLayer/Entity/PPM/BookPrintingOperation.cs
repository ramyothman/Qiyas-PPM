
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class BookPrintingOperation
    {
        [Display(Name = "ExamCode")]
        public string ExamCode
        {
            set { this.entity.Exam.ExamCode = value; }
            get 
            {
                if (!this.HasObject)
                    return "";
                if (this.entity.Exam == null)
                    return "";
                return this.entity.Exam.ExamCode; 
            }
        }
    }
}
      