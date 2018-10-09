using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrundtvigsDyreklinik.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using GrundtvigsDyreklinikTests1;

namespace GrundtvigsDyreklinik.Controllers.Tests
{
    [TestClass()]
    public class RacesControllerTests
    {
        [TestMethod()]
        public void GetPetDetails()
        {
            //Pets id cannot be null, if this test fails, 
            //it means that that the method Details() doesn't work or the pets table is empty (or no connection to db to database)
            PetsController pets = new PetsController();
            ActionResult result = pets.Details(3);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod()]
        public void GetRaceDetails()  
        {
            //Races details id cannot be null, if this test fails, 
            //it means that that the method Details() doesn't work or the races table is empty (or no conn to database)
            RacesController races = new RacesController();
            ActionResult result = races.Details(1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod()]
        public void GetTreatmentsListFromDB()
        {
            //Test to see if it's possible to get treatments from DB 
            TreatmentsController treatments = new TreatmentsController();
            ActionResult result = treatments.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]         
        public void TreatmentValidationOnCreate()
        {
                CheckPropertyValidation cpv = new CheckPropertyValidation();
                var trm = new Models.Treatment
                {
                    Name = "",
                    Comment = "Dette er en behandling for dyr med laser ",                                                                              
                };
                var errorcount = cpv.myValidation(trm).Count();
                Assert.AreNotEqual(0, errorcount);
        }



        [TestMethod()]
        public void SpeciesValidationOnCreate()
        {
            //Checking property validation for a new Treatment object, should pass when the properties aren't empty
            CheckPropertyValidation cpv = new CheckPropertyValidation();
            var species = new Models.Species
            {
                Name = "Hund",              
            };
            var errorcount = cpv.myValidation(species).Count();
            Assert.AreEqual(0, errorcount);

        }

        
    }

}


