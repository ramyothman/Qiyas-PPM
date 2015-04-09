
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

        private List<BusinessLogicLayer.Entity.PPM.ExamModelItem> _ExamModels;
        public List<BusinessLogicLayer.Entity.PPM.ExamModelItem> ExamModels
        {
            set { _ExamModels = value; }
            get
            {
                if(_ExamModels == null)
                {
                    if(this.HasObject)
                    {
                        _ExamModels = this.entity.ExamModelItems.Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ExamModelItem(c) { context = this.context }).ToList();
                    }
                    
                    if (_ExamModels == null)
                        _ExamModels = new List<ExamModelItem>();
                }
                return _ExamModels;
            }
        }

        



    }
}
      