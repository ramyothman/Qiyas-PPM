
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.ComponentModel;

namespace Qiyas.BusinessLogicLayer.Components.PPM
{
    public partial class ContainerRequestPackLogic
    {
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ContainerRequestPack> GetAllByRequestID(int ID)
        {
            return db.ViewContainerRequests.Where(c => c.ExamCenterRequiredExamsID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ContainerRequestPack() { context = db, CreatedDate = c.CreatedDate, ExamID = c.ExamID,BookPackItemID = c.BookPackItemID, ContainerRequestID = c.ContainerRequestID, ContainerRequestPackID = c.ContainerRequestPackID, ExamCode = c.ExamCode, ExamModelName = c.ExamModelName, PackCode = c.PackCode, PackSerial = c.PackSerial.Value, BooksCount = c.BooksCount.Value, PrintsForOneModel = c.PrintsForOneModel, ExamsNeededForA3 = c.ExamsNeededForA3, TotalExamModels = c.TotalExamModels, PackageTypeBooksPerPackage = c.PackageTypeBooksPerPackage, PackageTypeExamModelCount = c.PackageTypeExamModelCount, PackageTypeTotal = c.PackageTypeTotal }).ToList();
        }

        public bool PackExists(int id)
        {
            var item = db.ViewContainerRequests.Where(c => c.BookPackItemID == id).FirstOrDefault();
            return  !(item == null);
        }
    }
}
      