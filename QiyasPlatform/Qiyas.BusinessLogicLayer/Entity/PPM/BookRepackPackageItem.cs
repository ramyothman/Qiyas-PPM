
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class BookRepackPackageItem
    {
        private string _ExamCode;
        public string ExamCode
        {
            set { _ExamCode = value; }
            get { return _ExamCode; }
        }

        private string _ExamModelName;
        public string ExamModelName
        {
            set { _ExamModelName = value; }
            get { return _ExamModelName; }
        }

        private int? _TotalPacks;
        public int? TotalPacks
        {
            set { _TotalPacks = value; }
            get { return _TotalPacks; }
        }

        private int? _BooksCount;
        public int? BooksCount
        {
            set { _BooksCount = value; }
            get { return _BooksCount; }
        }

        private int? _PackSerial;
        public int? PackSerial
        {
            set { _PackSerial = value; }
            get { return _PackSerial; }
        }

        private string _PackCode;
        public string PackCode
        {
            set { _PackCode = value; }
            get { return _PackCode; }
        }

        private int? _ExamID = 0;
        public int? ExamID
        {
            set
            {
                _ExamID = value;
            }
            get
            {
                return _ExamID;
            }
        }

        private int? _PrintsForOneModel;
        public int? PrintsForOneModel 
        {
            set { _PrintsForOneModel = value; }
            get { return _PrintsForOneModel; }
        }

        private int? _ExamsNeededForA3;
        public int? ExamsNeededForA3
        {
            set { _ExamsNeededForA3 = value; }
            get { return _ExamsNeededForA3; }
        }

        private int? _TotalExamModels;
        public int? TotalExamModels
        {
            set { _TotalExamModels = value; }
            get { return _TotalExamModels; }
        }

        private int? _PackageTypeTotal;
        public int? PackageTypeTotal
        {
            set { _PackageTypeTotal = value; }
            get { return _PackageTypeTotal; }
        }

        private int? _PackageTypeBooksPerPackage;
        public int? PackageTypeBooksPerPackage
        {
            set { _PackageTypeBooksPerPackage = value; }
            get { return _PackageTypeBooksPerPackage; }
        }

        private int? _PackageTypeExamModelCount;
        public int? PackageTypeExamModelCount
        {
            set { _PackageTypeExamModelCount = value; }
            get { return _PackageTypeExamModelCount; }
        }
            
    }
}
      