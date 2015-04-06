
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class ExamPeriod
    {
        private string _StartDateHijri;
        public string StartDateHijri
        {
            set
            {
                _StartDateHijri = value;
                DateTime current = DateTime.MinValue;
                if(!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        current = Tools.HijriToGreg(value);
                        if (current != DateTime.MinValue)
                            StartDate = current;
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }
            get
            {
                if (StartDate.HasValue)
                {
                    if (StartDate.Value > DateTime.MinValue)
                    {
                        _StartDateHijri = Tools.GregToHijri(StartDate.Value);
                    }
                }
                return _StartDateHijri;
            }
        }

        private string _EndDateHijri;
        public string EndDateHijri
        {
            set
            {
                _EndDateHijri = value;
                DateTime current = DateTime.MinValue;
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        current = Tools.HijriToGreg(value);
                        if (current != DateTime.MinValue)
                            EndDate = current;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            get
            {
                if (EndDate.HasValue)
                {
                    if (EndDate.Value > DateTime.MinValue)
                    {
                        _EndDateHijri = Tools.GregToHijri(EndDate.Value);
                    }
                }
                return _EndDateHijri;
            }
        }

        
    }
}
      