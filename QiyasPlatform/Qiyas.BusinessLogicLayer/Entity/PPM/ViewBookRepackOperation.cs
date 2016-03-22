using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public class ViewBookRepackOperation : EntityBase<Qiyas.DataAccessLayer.ViewBookRepackOperation>
    {
        #region Constructors
        public ViewBookRepackOperation()
        {
            this.entity = new Qiyas.DataAccessLayer.ViewBookRepackOperation();
            isNew = true;
        }

        internal ViewBookRepackOperation(Qiyas.DataAccessLayer.ViewBookRepackOperation entity)
        {
            this.entity = entity;
        }
        #endregion

        [Display(Name = "BookPrintingOperationID")]
        public int BookPrintingOperationID
        {
            set { this.entity.BookPrintingOperationID = value; }
            get { return this.entity.BookPrintingOperationID; }
        }

        [Display(Name = "CalculationTypeName")]
        public string CalculationTypeName
        {
            set { this.entity.CalculationTypeName = value; }
            get { return this.entity.CalculationTypeName; }
        }

        [Display(Name = "PackagingTypeName")]
        public string PackagingTypeName
        {
            set { this.entity.PackagingTypeName = value; }
            get { return this.entity.PackagingTypeName; }
        }

        [Display(Name = "PackingValue")]
        public int? PackingValue
        {
            set { this.entity.PackingValue = value; }
            get { return this.entity.PackingValue; }
        }

        [Display(Name = "RepackCount")]
        public int? RepackCount
        {
            set { this.entity.RepackCount = value; }
            get { return this.entity.RepackCount; }
        }
    }
}
