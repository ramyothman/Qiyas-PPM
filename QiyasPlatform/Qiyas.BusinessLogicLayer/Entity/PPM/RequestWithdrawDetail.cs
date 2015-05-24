
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class RequestWithdrawDetail
    {
        private int _PackagingTypeID = 0;
        [Display(Name = "PackagingTypeID")]
        public int PackagingTypeID
        {
            set { this._PackagingTypeID = value; }
            get { return this._PackagingTypeID; }
        }

        private string _PackageName = "";
        [Display(Name = "PackagingTypeName")]
        public string PackageName
        {
            set { this._PackageName = value; }
            get { return this._PackageName; }
        }

        private int _PackCount = 0;
        [Display(Name = "PackCount")]
        public int PackCount
        {
            set { this._PackCount = value; }
            get { return this._PackCount; }
        }

        private string _ExamModel  = "";
        [Display(Name = "ExamModels")]
        public string ExamModel
        {
            set { this._ExamModel = value; }
            get { return this._ExamModel; }
        }

        
        private int _ExamCenterRequiredExamsID = 0;
        [Display(Name = "ExamCenterRequiredExamsID")]
        public int ExamCenterRequiredExamsID
        {
            set { this._ExamCenterRequiredExamsID = value; }
            get { return this._ExamCenterRequiredExamsID; }
        }

        private int? _ExamPeriodID = 0;
        [Display(Name = "ExamPeriodID")]
        public int? ExamPeriodID
        {
            set { this._ExamPeriodID = value; }
            get { return this._ExamPeriodID; }
        }

        private int _ExamCenterID = 0;
        [Display(Name = "ExamCenterID")]
        public int ExamCenterID
        {
            set { this._ExamCenterID = value; }
            get { return this._ExamCenterID; }
        }

        private string _ExamPeriodName = "";
        [Display(Name = "ExamPeriodName")]
        public string ExamPeriodName
        {
            set { this._ExamPeriodName = value; }
            get { return this._ExamPeriodName; }
        }

        private string _ExamCenterName = "";
        [Display(Name = "ExamCenterName")]
        public string ExamCenterName
        {
            set { this._ExamCenterName = value; }
            get { return this._ExamCenterName; }
        }

        private int? _ExamID = 0;
        [Display(Name = "ExamID")]
        public int? ExamID
        {
            set { this._ExamID = value; }
            get { return this._ExamID; }
        }

        private string _ExamCode = "";
        [Display(Name = "ExamCode")]
        public string ExamCode
        {
            set { this._ExamCode = value; }
            get { return this._ExamCode; }
        }

    }
}
      