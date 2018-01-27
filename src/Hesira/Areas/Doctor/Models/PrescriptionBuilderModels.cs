using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BForms.Models;
using BForms.Mvc;
using Hesira.Resources;

namespace Hesira.Areas.Doctor.Models
{
    #region Members - Supervisors & Workers Team Builders

    public class PrescriptionBuilderModel
    {

        public PrescriptionBuilderModel()
        {
            DrugsTab = new PrescriptionBuilderTabModel()
            {
                Grid = new BsGridModel<DrugViewModel>()
            };

            PrescriptionBuiler = new BsEditorGroupModel<DrugRowModel, DrugRowFormModel>
            {
                Form = new DrugRowFormModel()
            };
        }

        [BsEditorTab(Name = "Drugs", Id = PrescriptionType.Candidates, Selected = true)]
        public PrescriptionBuilderTabModel DrugsTab { get; set; }

        [BsEditorGroup(Id = PrescriptionType.Final)]
        public BsEditorGroupModel<DrugRowModel, DrugRowFormModel> PrescriptionBuiler { get; set; }


    }

    public class PrescriptionBuilderTabModel : BsEditorTabModel<DrugViewModel>
    {

    }

    // Used for supervisors/workers team builder
    public class DrugViewModel : BsGridRowModel<object>
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string Vendor { get; set; }
        public string GeneralName { get; set; }
        public override object GetUniqueID()
        {
            return this.Id;
        }

    }

    public class DrugRowModel : BsEditorGroupItemModel<DrugRowFormModel>
    {
        public DrugRowModel()
        {
            Form = new DrugRowFormModel();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public override object GetUniqueID()
        {
            return this.Id;
        }

    }

    public class DrugRowFormModel
    {

        public DrugRowFormModel()
        {
            NumberOfDays = new BsRangeItem<int?>
            {
                MinValue = 1,
                MaxValue = 30,
                ItemValue = 1,
                TextValue = "1"
            };
            Quantity= new BsRangeItem<int?>
            {
                MinValue = 1,
                MaxValue = 120,
                ItemValue = 1,
                TextValue = "1"
            };
        }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Days", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.NumberInline)]
        public BsRangeItem<int?> NumberOfDays { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Quantity", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.NumberInline)]
        public BsRangeItem<int?> Quantity { get; set; }
    }

    public enum PrescriptionType
    {
        Candidates = 1,
        Final = 2
    }

    #endregion


}