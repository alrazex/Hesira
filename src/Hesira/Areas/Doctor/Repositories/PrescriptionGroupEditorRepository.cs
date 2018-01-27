using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using BForms.Grid;
using Hesira.Areas.Doctor.Models;
using Hesira.Entities;

namespace Hesira.Areas.Doctor.Repositories
{

    public class PrescriptionGroupEditorRepositorySettings : BsGridBaseRepositorySettings
    {
        
    }

    public class PrescriptionGroupEditorRepository : BsBaseGridRepository<Drug, DrugViewModel>
    {

        #region Constructor and Properties

        private readonly HesiraEntities db;

        public PrescriptionGroupEditorRepositorySettings Settings
        {
            get { return settings as PrescriptionGroupEditorRepositorySettings; }
        }

        public PrescriptionGroupEditorRepository(HesiraEntities db)
        {
            this.db = db;
        }
        #endregion

        #region Filter/Order/Map

        public override IQueryable<Drug> Query()
        {

            var query = db.Drugs.AsQueryable();

            return Filter(query);
        }

        public override IOrderedQueryable<Drug> OrderQuery(IQueryable<Drug> query)
        {
            return query.OrderBy(x => x.ComercialName);
        }

        public override IEnumerable<DrugViewModel> MapQuery(IQueryable<Drug> query)
        {
            var currentList = query.Select(x => new DrugViewModel
            {
                Id = (long)x.Id,
                Name = x.ComercialName,
                Vendor = x.Vendor,
                GeneralName = x.GeneralName
            }).AsEnumerable();

            return currentList;
        }

        public IQueryable<Drug> Filter(IQueryable<Drug> query)
        {
            if (!string.IsNullOrEmpty(Settings.QuickSearch))
            {
                var searchTerm = Settings.QuickSearch.Trim();

                query =
                    query.Where(
                        x =>
                            x.ComercialName.Contains(searchTerm) || x.GeneralName.Contains(searchTerm) ||
                            x.OriginCountry.Contains(searchTerm) || x.Vendor.Contains(searchTerm));
            }

            return query;
        }

        #endregion

    }



}