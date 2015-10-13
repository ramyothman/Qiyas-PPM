
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
    

    public partial class RequestWithdrawLogic
    {
        public int GetSerial(int ID)
        {
           return db.GetWithdrawRequestSerial(ID).Value;
        }

        public List<TotalRemainingPacks> GetRemainingPacks()
        {
            var totalPacks = db.ViewCurrentPackStockExistences.ToList();
            var totalReserved = db.ViewReservedPacks.ToList();
            var remainingPacks = new List<TotalRemainingPacks>();
            foreach(var item in totalPacks)
            {
                TotalRemainingPacks pack = new TotalRemainingPacks();
                pack.ExamID = item.ExamID.Value;
                pack.PackagingTypeID = item.PackagingTypeID;
                pack.BooksPerPackage = item.BooksPerPackage.Value;
                pack.ExamModelCount = item.ExamModelCount.Value;

                var temp = totalReserved != null? totalReserved.Where(c => c.ExamID == item.ExamID && c.PackagingTypeID == item.PackagingTypeID).FirstOrDefault() : null;
                if(temp != null && item.PackCount != null)
                {
                    if (temp.PackCount.HasValue)
                        pack.PackCount = item.PackCount.Value - temp.PackCount.Value;
                    else
                        pack.PackCount = item.PackCount.Value;
                }
                else if(temp == null && item.PackCount != null)
                {
                    pack.PackCount = item.PackCount.Value;
                }
                else
                    pack.PackCount = 0;

                remainingPacks.Add(pack);
            }
            return remainingPacks;

        }

        public bool HaveEnoughPacks(int TotalBookCount, int ExamID, List<TotalRemainingPacks> remainingItems)
        {
            bool result = false;
            var totals = (from x in remainingItems where x.ExamID == ExamID select x);
            int totalValue = 0;
            foreach(var item in totals)
            {
                if(item.ExamModelCount > 1)
                {
                    totalValue += item.PackCount * item.BooksPerPackage;
                }
                else
                {
                    totalValue += (item.PackCount/ item.ExamModelCount) * item.BooksPerPackage;
                }
            }
            result = TotalBookCount >= totalValue;
            return result;
        }

        public List<BusinessLogicLayer.Entity.PPM.RequestWithdrawDetailItem> GetPacksForWithdrawalTotals(BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail detail, int ExamID, List<BusinessLogicLayer.Entity.PPM.ExamModelItem> models, List<TotalRemainingPacks> remainingItems)
        {
            int TotalBookCount = detail.PrintsForOneModel.Value;
            int TotalA3 = detail.ExamsNeededForA3.Value;
            int TotalCD = detail.ExamsNeededForCD.Value;
            List<BusinessLogicLayer.Entity.PPM.RequestWithdrawDetailItem> requestDetailItems = new List<Entity.PPM.RequestWithdrawDetailItem>();

            var itemsRemainingSingle  = from x in remainingItems where x.ExamID == ExamID && x.ExamModelCount == 1 orderby x.BooksPerPackage descending select x;
            var itemsRemainingMulti = from x in remainingItems where x.ExamID == ExamID && x.ExamModelCount > 1 orderby x.BooksPerPackage descending select x;
            int remainingValue = TotalBookCount;
            int remainingValueA3 = TotalA3;
            foreach(var item in itemsRemainingSingle)
            {
                if(item.BooksPerPackage == 3 && item.ExamModelCount == 1)
                {
                    if (remainingValueA3 <= 0)
                        break;
                    int packsNeeded = remainingValueA3 / item.BooksPerPackage;
                    if (packsNeeded == 0)
                        continue;
                    if (packsNeeded <= item.PackCount)
                    {
                        BusinessLogicLayer.Entity.PPM.RequestWithdrawDetailItem witem = new Entity.PPM.RequestWithdrawDetailItem();
                        witem.PackagingTypeID = item.PackagingTypeID;
                        witem.PackCount = packsNeeded;
                        foreach (var model in models)
                        {
                            BusinessLogicLayer.Entity.PPM.RequestWithdrawDetailItemModel examModel = new Entity.PPM.RequestWithdrawDetailItemModel();
                            examModel.ExamModelID = model.ExamModelID;
                            witem.Models.Add(examModel);
                        }
                        requestDetailItems.Add(witem);
                        remainingValueA3 -= (packsNeeded * item.BooksPerPackage);
                    }
                }
                else
                {
                    int packsNeeded = remainingValue / item.BooksPerPackage;
                    if (packsNeeded == 0)
                        continue;
                    foreach (var model in models)
                    {
                        if (remainingValue <= 0)
                            break;

                        if (packsNeeded <= item.PackCount)
                        {
                            BusinessLogicLayer.Entity.PPM.RequestWithdrawDetailItem witem = new Entity.PPM.RequestWithdrawDetailItem();
                            witem.PackagingTypeID = item.PackagingTypeID;
                            witem.PackCount = packsNeeded;
                            BusinessLogicLayer.Entity.PPM.RequestWithdrawDetailItemModel examModel = new Entity.PPM.RequestWithdrawDetailItemModel();
                            examModel.ExamModelID = model.ExamModelID;
                            witem.Models.Add(examModel);
                            requestDetailItems.Add(witem);

                        }
                    }
                    remainingValue -= (packsNeeded * item.BooksPerPackage);
                }
                
            }

            foreach(var item in itemsRemainingMulti)
            {
                if (remainingValue <= 0)
                    break;
                int packsNeeded = remainingValue / item.BooksPerPackage;
                if (packsNeeded <= item.PackCount)
                {
                    BusinessLogicLayer.Entity.PPM.RequestWithdrawDetailItem witem = new Entity.PPM.RequestWithdrawDetailItem();
                    witem.PackagingTypeID = item.PackagingTypeID;
                    witem.PackCount = packsNeeded;
                    foreach (var model in models)
                    {
                        BusinessLogicLayer.Entity.PPM.RequestWithdrawDetailItemModel examModel = new Entity.PPM.RequestWithdrawDetailItemModel();
                        examModel.ExamModelID = model.ExamModelID;
                        witem.Models.Add(examModel);
                    }
                    requestDetailItems.Add(witem);
                    remainingValue -= (packsNeeded * item.BooksPerPackage);
                }
            }
            

            return requestDetailItems;
        }
    }

    public class TotalRemainingPacks
    {
        public int PackCount { set; get; }
        public int PackagingTypeID { set; get; }

        public int ExamModelCount { set; get; }
        public int BooksPerPackage { set; get; }
        public int ExamID { set; get; }
    }
}
      