
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

        private int? _ModelCount;
        [Display(Name = "ModelCount")]
        public int ModelCount
        {
            set { _ModelCount = value; }
            get
            {
                if(!_ModelCount.HasValue)
                {
                    _ModelCount = 0;
                }
                return _ModelCount.Value;
            }
        }

        private int? _TotalPacks;
        [Display(Name = "TotalPacks")]
        public int TotalPacks
        {
            set { _TotalPacks = value; }
            get
            {
                if (!_TotalPacks.HasValue)
                {
                    _TotalPacks = 0;
                }
                return _TotalPacks.Value;
            }
        }

        private string _PackNumber;
        [Display(Name = "PackNumber")]
        public string PackNumber
        {
            set { _PackNumber = value; }
            get
            {
                if (!string.IsNullOrEmpty(_PackNumber))
                {
                    _PackNumber = "";
                }
                return _PackNumber;
            }
        }


    }
}
      