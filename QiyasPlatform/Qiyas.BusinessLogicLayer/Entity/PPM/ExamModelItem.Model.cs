
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
    public partial class ExamModelItem : EntityBase<Qiyas.DataAccessLayer.ExamModelItem>
    {

        #region Constructors
        public ExamModelItem()
        {
            this.entity = new Qiyas.DataAccessLayer.ExamModelItem();
            isNew = true;
        }

        public ExamModelItem(int ExamModelItemID)
        {
            this.entity = context.ExamModelItems.Where(p => p.ExamModelItemID == ExamModelItemID).FirstOrDefault();  
        }
    
        internal ExamModelItem(Qiyas.DataAccessLayer.ExamModelItem entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamModelItemID")]
        public int ExamModelItemID
        {            
            set{ this.entity.ExamModelItemID = value; }
            get{ return this.entity.ExamModelItemID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamID")]
        public int? ExamID
        {            
            set{ this.entity.ExamID = value; }
            get{ return this.entity.ExamID; }
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
              context.ExamModelItems.InsertOnSubmit(this.entity);
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
              context.ExamModelItems.DeleteOnSubmit(this.entity);
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
      