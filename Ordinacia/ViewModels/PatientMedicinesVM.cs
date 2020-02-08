using System.Collections.Generic;
using Ordinacia.Data_Access;

namespace Ordinacia.ViewModels
{
    public class PatientMedicinesVM
    {
        public ICollection<Medicine> Medicines { get; set; }
        public int PatientId { get; set; }
    }
}