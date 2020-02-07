using System.Collections.Generic;
using Ordinacia.Data_Access;

namespace Ordinacia.ViewModels
{
    public class DocMedicinesVM
    {
        public int CurrentPatientId { get; set; }
        public ICollection<Medicine> Medicines { get; set; }
    }
}