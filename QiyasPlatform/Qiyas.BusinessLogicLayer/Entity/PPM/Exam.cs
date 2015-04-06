
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class Exam
    {
        private int? _ExamTypeID;
        [Required(ErrorMessage = "RequiredValidation")]
        [Display(Name = "ExamTypeID")]
        public int? ExamTypeID
        {
            set 
            { 
                    this._ExamTypeID = value; 
            }
            get { return _ExamTypeID; }
        }
    }
}
      