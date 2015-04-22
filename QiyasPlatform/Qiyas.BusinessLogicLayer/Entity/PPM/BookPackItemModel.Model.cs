
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    [DataObject(true)]
    [Serializable]
    public partial class BookPackItemModel : EntityBase<Qiyas.DataAccessLayer.BookPackItemModel>
    {

        #region Constructors
        public BookPackItemModel()
        {
            this.entity = new Qiyas.DataAccessLayer.BookPackItemModel();
            isNew = true;
        }

        public BookPackItemModel(int BookPackItemModelID)
        {
            this.entity = context.BookPackItemModels.Where(p => p.BookPackItemModelID == BookPackItemModelID).FirstOrDefault();  
        }
    
        internal BookPackItemModel(Qiyas.DataAccessLayer.BookPackItemModel entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "BookPackItemModelID")]
        public int BookPackItemModelID
        {            
            set{ this.entity.BookPackItemModelID = value; }
            get{ return this.entity.BookPackItemModelID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "BookPackItemID")]
        public int? BookPackItemID
        {            
            set{ this.entity.BookPackItemID = value; }
            get{ return this.entity.BookPackItemID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamModelID")]
        public int? ExamModelID
        {            
            set{ this.entity.ExamModelID = value; }
            get{ return this.entity.ExamModelID; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.BookPackItemModels.InsertOnSubmit(this.entity);
            }
            else
            {
                
            }
            if (commit)
      
              try
              {
                context.SubmitChanges();
                isNew = false;
                return true;
              }
              catch (Exception ex)
              {
                lastException = ex;
                return false;
              }
              else
                return null;
        }
    
        internal override bool? Delete(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (!isNew)
              context.BookPackItemModels.DeleteOnSubmit(this.entity);
            else
              return false;
            if (commit)
              try
              {
                context.SubmitChanges();
                return true;
              }
              catch (Exception ex)
              {
                lastException = ex;
                return false;
              }
            else
              return null;
        }
    
        public bool Save()
        {
            return Save(context, true).Value;
        }
    
        public bool Delete()
        {
            return Delete(context, true).Value;
        }
        #endregion
    }
}
      