
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
        [Display(Name = "ExamModelNames")]
        public string ExamModels
        {
            get
            {
                if (this.entity.ExamID == 0)
                    return "";
                var models = this.entity.Exam.ExamModelItems;
                int count = models.Count;
                int counter = 1;
                string result = "";
                foreach(var modelItem in models)
                {

                    result += modelItem.ExamModel.Name;
                    if(counter < count)
                        result += " - ";
                    counter++;
                }

                return result;
            }
        }
        [Display(Name = "ExamCode")]
        public string ExamCode
        {
            set { this.entity.Exam.ExamCode = value; }
            get 
            {
                if (!this.HasObject)
                    return "";
                if (this.entity.ExamID == 0)
                    return "";
                if (this.entity.Exam == null)
                    return "";
                return this.entity.Exam.ExamCode; 
            }
        }

        
        [Display(Name = "ModelCount")]
        public int? ModelCount
        {
            
            get
            {
                return this.entity.TotalExamModels;
            }
        }

        
        [Display(Name = "TotalPacks")]
        public int? TotalPacks
        {
            
            get
            {
                return this.entity.TotalPacks;
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

        public void UpdateItemPackStatus(int StatusID)
        {
            context.UpdateItemPacksBulk(BookPrintingOperationID, StatusID);
        }


    }
}
      