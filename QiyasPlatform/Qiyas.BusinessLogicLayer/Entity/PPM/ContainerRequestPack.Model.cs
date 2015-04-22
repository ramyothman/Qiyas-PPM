
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
    public partial class ContainerRequestPack : EntityBase<Qiyas.DataAccessLayer.ContainerRequestPack>
    {

        #region Constructors
        public ContainerRequestPack()
        {
            this.entity = new Qiyas.DataAccessLayer.ContainerRequestPack();
            isNew = true;
        }

        public ContainerRequestPack(int ContainerRequestPackID)
        {
            this.entity = context.ContainerRequestPacks.Where(p => p.ContainerRequestPackID == ContainerRequestPackID).FirstOrDefault();  
        }
    
        internal ContainerRequestPack(Qiyas.DataAccessLayer.ContainerRequestPack entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ContainerRequestPackID")]
        public int ContainerRequestPackID
        {            
            set{ this.entity.ContainerRequestPackID = value; }
            get{ return this.entity.ContainerRequestPackID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ContainerRequestID")]
        public int? ContainerRequestID
        {            
            set{ this.entity.ContainerRequestID = value; }
            get{ return this.entity.ContainerRequestID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "BookPackItemID")]
        public int? BookPackItemID
        {            
            set{ this.entity.BookPackItemID = value; }
            get{ return this.entity.BookPackItemID; }
        }
    
        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate
        {            
            set{ this.entity.CreatedDate = value; }
            get{ return this.entity.CreatedDate; }
        }
    
        [Display(Name = "CreatedBy")]
        public DateTime? CreatedBy
        {            
            set{ this.entity.CreatedBy = value; }
            get{ return this.entity.CreatedBy; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.ContainerRequestPacks.InsertOnSubmit(this.entity);
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
              context.ContainerRequestPacks.DeleteOnSubmit(this.entity);
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
      