
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class BookPackItem
    {

        public BookPackItem(bool getIdentity)
        {
            
            this.entity = new Qiyas.DataAccessLayer.BookPackItem();
            var itemLast = context.BookPackItems.OrderByDescending(x => x.BookPackItemID).Select(x => x.BookPackItemID).FirstOrDefault();
            BookPackItemID = itemLast + 1;
            isNew = true;
        }

        public BookPackItem(bool getIdentity, int key)
        {
            if(getIdentity)
            {
                this.entity = new Qiyas.DataAccessLayer.BookPackItem();
                var itemLast = context.BookPackItems.OrderByDescending(x => x.BookPackItemID).Select(x => x.BookPackItemID).FirstOrDefault();
                BookPackItemID = itemLast + 1;
                isNew = true;
            }
            else
            {
                this.entity = new Qiyas.DataAccessLayer.BookPackItem();
                //var itemLast = context.BookPackItems.OrderByDescending(x => x.BookPackItemID).Select(x => x.BookPackItemID).FirstOrDefault();
                BookPackItemID = key;
                isNew = true;
            }
            
        }

        public BookPackItem(string PackageCode)
        {
            this.entity = context.BookPackItems.Where(p => p.PackCode == PackageCode).FirstOrDefault();  
        }
        private string _Speciality;
        public string Speciality
        {
            set { _Speciality = value; }
            get{ return _Speciality;}
        }

        private string _ModelandNumber;
        public string ModelandNumber
        {
            set { _ModelandNumber = value; }
            get { return _ModelandNumber; }
        }

        private string _PackageTypeName;
        public string PackageTypeName
        {
            set { _PackageTypeName = value; }
            get { return _PackageTypeName; }
        }

        private int _LastBookCount;
        public int LastBookCount
        {
            set { _LastBookCount = value; }
            get { return _LastBookCount; }
        }

        private List<BusinessLogicLayer.Entity.PPM.BookPackItemModel> _ItemModels;
        public List<BusinessLogicLayer.Entity.PPM.BookPackItemModel> ItemModels
        {
            set { _ItemModels = value; }
            get 
            { 
                if(_ItemModels == null)
                {
                    _ItemModels = context.BookPackItemModels.Where(c => c.BookPackItemID == BookPackItemID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemModel(c) { context = this.context }).ToList();
                    if (_ItemModels == null)
                        _ItemModels = new List<BookPackItemModel>();
                }
                return _ItemModels;
            }
        }

        private string _ExamModelName;
        public string ExamModelName
        {
            set { _ExamModelName = value; }
            get
            {
                if(string.IsNullOrEmpty(_ExamModelName))
                 _ExamModelName = context.GetExamModelNameFromPackItem(BookPackItemID);
                return _ExamModelName;
            }
        }


    }
}
      